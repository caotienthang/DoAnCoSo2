using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnCoSo2.Models;

namespace DoAnCoSo2.Controllers
{
    public class DeThisController : Controller
    {
        private ThiTNEntities db = new ThiTNEntities();

        // GET: DeThis
        public ActionResult Index()
        {
            return View(db.DeThis.ToList());
        }

        // GET: DeThis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeThi deThi = db.DeThis.Find(id);
            if (deThi == null)
            {
                return HttpNotFound();
            }
            return View(deThi);
        }

        // GET: DeThis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeThis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDT,NgayThi,ThoiGianThi,SoLuongCauHoi")] DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                db.DeThis.Add(deThi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deThi);
        }
        public void LayBaiThi(int id)
        {
            List<DeThi_CauHoi> qs = (from x in db.DeThi_CauHoi
                                     where x.MaDT == id
                                     select x).OrderBy(x => Guid.NewGuid()).ToList();
            foreach (var item in qs)
            {
                var StudentTest = new BaiLam();
                StudentTest.MaDT = id;
                StudentTest.MaCH = item.MaCH;
                CauHoi q = db.CauHois.SingleOrDefault(x => x.MaCH == item.MaCH);
                StudentTest.DeBai = q.CauHoi1;
                string[] answer = { q.CauA, q.CauB, q.CauC, q.CauD };
                StudentTest.CauA = answer[0];
                StudentTest.CauB = answer[1];
                StudentTest.CauC = answer[2];
                StudentTest.CauD = answer[3];
                db.BaiLams.Add(StudentTest);
                db.SaveChanges();
            }
        }
        public ActionResult VaoThi(int id)
        {
            LayBaiThi(id);
            var baithi = db.BaiLams.Where(m => m.MaDT == id);
            return View(baithi);
        }
        public List<StudentQuestViewModel> GetListQuest(int ?id)
        {
            List<StudentQuestViewModel> list = new List<StudentQuestViewModel>();
            try
            {
                list = (from x in db.BaiLams
                        join t in db.DeThis on x.MaDT equals t.MaDT
                        join q in db.CauHois on x.MaCH equals q.MaCH
                        where x.MaDT == id
                        select new StudentQuestViewModel { DeThi = t, BaiLam = x, CauHoi = q }).OrderBy(x => x.BaiLam.MaBL).ToList();
            }
            catch (Exception) { }
            return list;
        }
        public ActionResult SubmitTest()
        {
            var sv=db.DeThi_SInhVien.FirstOrDefault(m=>m.MaDT==1);
            var list = GetListQuest(sv.MaDT);
            int total_quest = (int)list.First().DeThi.SoLuongCauHoi;
            int test_code = list.First().DeThi.MaDT;
            double coefficient = 10.0 / (double)total_quest;
            int count_correct = 0;
            foreach (var item in list)
            {
                if (item.BaiLam.DapAn != null && item.BaiLam.DapAn.Trim().Equals(item.CauHoi.DapAn.Trim()))
                    count_correct++;
            }
            double score = coefficient * count_correct;
            string detail = count_correct + "/" + total_quest;
            InsertScore(score, detail);
            return RedirectToAction("PreviewTest/" + test_code);
        }
        [HttpPost]
        public void UpdateStudentTest(FormCollection form)
        {
            int id_quest = Convert.ToInt32(form["id"]);
            string answer = form["answer"];
            answer = answer.Trim();
            UpdateStudentTest(id_quest, answer);
        }
        public void UpdateStudentTest(int id_question, string answer)
        {
            var update = db.BaiLams.FirstOrDefault(m => m.MaCH == id_question);
            update.DapAn = answer;
            db.SaveChanges();
        }
        public ActionResult PreviewTest(int id)
        {
            ViewBag.score = GetScore(id);
            return View(GetListQuest(id));
        }
        public Diem GetScore(int test_code)
        {
            Diem score = new Diem();
            try
            {
                score = db.Diems.SingleOrDefault(x => x.MaDT == test_code);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return score;
        }
        public void InsertScore(double score, string detail)
        {
            var s = new Diem();
            s.Diem1 = (decimal?)score;
            s.ThoiGianLam = 60;
            s.MSSV = "2080601167";
            db.Diems.Add(s);
            db.SaveChanges();
        }
        
        // GET: DeThis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeThi deThi = db.DeThis.Find(id);
            if (deThi == null)
            {
                return HttpNotFound();
            }
            return View(deThi);
        }

        // POST: DeThis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDT,NgayThi,ThoiGianThi,SoLuongCauHoi")] DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deThi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deThi);
        }

        // GET: DeThis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeThi deThi = db.DeThis.Find(id);
            if (deThi == null)
            {
                return HttpNotFound();
            }
            return View(deThi);
        }

        // POST: DeThis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeThi deThi = db.DeThis.Find(id);
            db.DeThis.Remove(deThi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
