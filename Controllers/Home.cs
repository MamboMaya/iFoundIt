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
    public IAuthService auth;



    [HttpGet]
    [AllowAnonymous]
    public IActionResult Root() => View("Index");

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login() => View("RegisterOrLogin");

    [HttpPost("login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromForm] LoginVM user){
        string result = await auth.Login(user.Email, user.Password);
        if(result == null) { 
            return Redirect("/");
        }
        ModelState.AddModelError(" ", result);
        return View("Login", user);
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
