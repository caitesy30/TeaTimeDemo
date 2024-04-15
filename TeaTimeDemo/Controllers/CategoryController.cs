using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTimeDemo.Data;
using TeaTimeDemo.Models;

namespace TeaTimeDemo.Controllers
{




    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
 

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {

            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {

            // 創建一個新的 Category 實例
            var category = new TeaTimeDemo.Models.Category();
            // 將模型傳遞給視圖
            return View(category);
            //  return View();
        }

 



        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "類別名稱不能跟顯示順序一致。");
            }
            if (ModelState.IsValid && obj.Name != null)  // 確保名稱不是空的
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["SUCCESS"] = "類別新增成功!";
                return RedirectToAction("Index");  // 假設有一個叫做 Index 的動作方法
            }

            // 如果模型狀態無效或名稱是空的，返回到創建頁面並顯示錯誤
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
        
            return View("Edit", categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["SUCCESS"] = "更新成功!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            { 
                return NotFound();
            }
            Category categoryFromDb= _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj= _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["SUCCESS"] = "刪除成功!";
            return RedirectToAction("Index");
        }
    }
}
