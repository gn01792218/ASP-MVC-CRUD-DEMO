using BulkyBookWep.Data;
using BulkyBookWep.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWep.Controllers;

public class CategoryController : Controller
{
    //宣告DB 實例
    private readonly DataContext _db;

    //依賴注入DB
    public CategoryController(DataContext db)
    {
        _db = db;
    }

    //1.在Index頁面中獲取DB資料，以展示Category
    public IActionResult Index()
    {
        var categoryList = _db.Categories.ToList();
        // IEnumerable<Category> categoryList = _db.Categories.ToList();
        return View(categoryList);
    }

    //2-1. Create頁面 (GET) - 呈現表單
    public IActionResult Create()
    {
        return View();
    }

    //2-2. Create頁面 (POST) - 送出表單
    [HttpPost]
    [ValidateAntiForgeryToken] //防止跨域攻擊
    public IActionResult Create(Category category)
    {
        //客製化的Error
        //符合這個錯誤的話，就會顯示在前端的asp-validation-summary中
        if (category.Name == category.DisplayOrder.ToString())
        {
            //輸入參數為 key、errorMessage
            //1.key值如果 隨意輸入，則只會顯示在asp-validation-summary中
            //2.key值如果輸入 對應的Model中的Property名稱
            //就會在該asp-validation-for中也顯示出來
            ModelState.AddModelError("Name", "姓名和顯示順序不可以是一樣的!");
        }

        //驗證是否有空值，再寫入DB
        if (ModelState.IsValid) //ModelState.IsValid會自動檢查是否符合Model中各個欄位
        {
            _db.Categories.Add(category);
            _db.SaveChanges(); //必須要Save才會真的存進去唷
            return RedirectToAction("Index");
        }

        //否則原物件返回當前頁面
        return View(category);
    }

    //3-1 Edit頁面 (GET)
    //依據要編輯的Category id 顯示要編輯的 Category
    public IActionResult Edit(int? categoryId) //這個參數可以透過 asp-route來獲取，只要再後綴這個參數名稱即可( asp-route-categoryId )
    {
        if (categoryId == null || categoryId == 0) return NotFound();
        var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category == null) return NotFound();

        return View(category);
    }

    //3-2 Edit頁面(Post)
    [HttpPost]
    [ValidateAntiForgeryToken] //防止跨域攻擊
    public IActionResult Edit(Category category)
    {
        //客製化的Error
        //符合這個錯誤的話，就會顯示在前端的asp-validation-summary中
        if (category.Name == category.DisplayOrder.ToString())
        {
            //輸入參數為 key、errorMessage
            //1.key值如果 隨意輸入，則只會顯示在asp-validation-summary中
            //2.key值如果輸入 對應的Model中的Property名稱
            //就會在該asp-validation-for中也顯示出來
            ModelState.AddModelError("Name", "姓名和顯示順序不可以是一樣的!");
        }

        //驗證是否有空值，再寫入DB
        if (ModelState.IsValid) //ModelState.IsValid會自動檢查是否符合Model中各個欄位
        {
            _db.Categories.Update(category);
            _db.SaveChanges(); //必須要Save才會真的存進去唷
            return RedirectToAction("Index");
        }

        //否則原物件返回當前頁面
        return View(category);
    }

    //4-1 Delete頁面 (GET)
    //依據要編輯的Category id 顯示要刪除的 Category
    public IActionResult Delete(int? categoryId) //這個參數可以透過 asp-route來獲取，只要再後綴這個參數名稱即可( asp-route-categoryId )
    {
        if (categoryId == null || categoryId == 0) return NotFound();
        var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);
        if (category == null) return NotFound();

        return View(category);
    }

    //4-2 Delete頁面(Post)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken] //防止跨域攻擊
    public IActionResult DeletePost(int? categoryId)
    {
        var category = _db.Categories.SingleOrDefault(c=>c.Id == categoryId);
        if (category == null) return NotFound();
        _db.Categories.Remove(category);
        _db.SaveChanges(); //必須要Save才會真的存進去唷
        return RedirectToAction("Index");
    }
}