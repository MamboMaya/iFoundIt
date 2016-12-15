using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

[Route("/")]
[Authorize]
public class HomeController : Controller
{
    private DB db;
    private IRepository<Finder> finder;
    private IRepository<Item> item;
    public IAuthService auth;
    public HomeController(DB db, IRepository<Finder> finder, IRepository<Item> item, IAuthService auth){
        this.db = db;
        this.finder = finder;
        this.item = item;
        this.auth = auth;
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Root() => View("Index");

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login() => View("LoginOrRegister");

    [HttpPost("login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginVM user){
        string result = await auth.Login(user.Email, user.Password);
        if(result == null) { 
        return Redirect("/finder/1"); //THIS LEADS BACK TO MyHouse ACCOUNT PAGE AT ALL TIMES!!    
        }
        ModelState.AddModelError(" ", result);
        return View("LoginOrRegister", user);
    }
    [HttpGet("contact")]
    [AllowAnonymous]
    public IActionResult Contact() => View("Contact");

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromForm] LoginVM user){
        var errors = await auth.Register(user.Email, user.Password);
        if((errors ?? new List<string>()).Count() == 0) {
            return Redirect("/finder/new");
        } else {
            foreach(var e in errors)
                ModelState.AddModelError("", e);

                return View("LoginOrRegister");
        }
    }

    [HttpGet("finder/new")]
    public IActionResult CreateFinder() => View("CreateFinder");

    [HttpPost("finder/new")]
    [ValidateAntiForgeryToken] 
    public IActionResult CreateFinder([FromForm] Finder finder){
        if(!ModelState.IsValid)
            return View("CreateFinder", finder);

            db.Finders.Add(finder);
            db.SaveChanges();
            return Redirect("/finder/{id}");            
    }
    
    [HttpGet("finder/{id}")]
    public IActionResult Finder(int id){
        Finder item = finder.Read(id);
        if(item == null) return NotFound();
        return View("Finder", item);
    }

    [HttpPost("finder/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult PostNewItem([FromForm] Item i, int id){
        // i.Finder = null;
        // string name = (await auth.GetUser(user.Name));
        // i.Finder = new Finder {Name = name};       //THIS DOES NOT APPLY TO THIS PROJECT. THE FINDER IS POSTER

        TryValidateModel(i);

        if(ModelState.IsValid){
            db.Items.Add(i);
            db.SaveChanges();
        }

        return Redirect($"/finder/{id}");
    }

    [HttpGet("item/{id}")]
    [AllowAnonymous]

    public IActionResult SingleItem(int id) {
        var x = item.Read(id);
        return View("SingleItem", x);
    }

    // [HttpGet("search")]
    // [AllowAnonymous]
    // public IActionResult SearchScreen(){
        
    //     return View("Search", db.Items.ToList());
    // }   

    [HttpGet("search")]
    [AllowAnonymous]
    public IActionResult Search(string query){
        return View("Search", db.Items.ToList());
    }


    [HttpPost("search/results")]
    [AllowAnonymous]
    public IActionResult SearchResult([FromForm]string query){
        // List<Item> Items2 = new List<Item>();
        var a = db.Items.Where(x=>x.Description==query).ToList();
            a.Log();
            return View("SearchResult", a);
    }
   
    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {
        await auth.Logout();
        return Redirect("/");
    }

}
public class LoginVM {
    [Required]
    [EmailAddress]
    public string Email {get; set;}
    [Required]
    [DataType(DataType.Password)]
    public string Password {get; set;}
}

    
    // Handle file uploads?
    // <form method="post" enctype="multipart/form-data">
    //     <input type="file" name="files" id="files" multiple />
    //     <input type="submit" value="submit" />
    // </form>
    // [HttpPost]
    // public async Task<IActionResult> Index(IList<IFormFile> files)
    // {
    //     foreach (var file in files)
    //     {
    //         var fileName = ContentDispositionHeaderValue
    //             .Parse(file.ContentDisposition)
    //             .FileName
    //             .Trim('"');// FileName returns "fileName.ext"(with double quotes) in beta 3

    //         if (fileName.EndsWith(".txt"))// Important for security if saving in webroot
    //         {
    //             // take file and store as Postgres Blob
    //         }
    //     }
    //     return RedirectToAction("Index");
    // }
