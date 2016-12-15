using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Item : HasId
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
    // [Required]
    [StringLength(100, MinimumLength = 5)]
    private string SecretDetails { get; set; }
    public DateTime FoundOn { get; set;} = new DateTime ();
    public int FinderId {get; set; } //foreign key
    public Finder Finder {get; set;} //foreign key
    public bool IsNotClaimed { get; set;}
    public string getSecret(IdentityUser u){
        // if(Finder.User.Id == u.Id) {
        //     return SecretDetails;
        // }
        return null;
    }
}

public class Finder : HasId {
    [Required]
    public string Name {get; set;}
    [Required]
    public int Id { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public int ZIP { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    public int ItemId { get; set; }
    public List<Item> Items {get; set;} = new List<Item>();
    // public IdentityUser User {get;set;}
    // public List<Item> ClaimedItems {get; set;}
}

public class Loser {
    [Required]
    public int Id { get; set; }
}

// declare the DbSet<T>'s of our DB context, thus creating the tables
public partial class DB : IdentityDbContext<IdentityUser> {
    public DbSet<Item> Items { get; set; }
    // public DbSet<Item> ClaimedItems { get; set; }
    public DbSet<Finder> Finders { get; set; }
}

// create a Repo<T> services
public partial class Handler {      
    public void RegisterRepos(IServiceCollection services){
        Repo<Finder>.Register(services, "Finders",
            dbset => dbset.Include(x => x.Items));
        Repo<Item>.Register(services, "Items",
            dbset => dbset.Include(x => x.Finder));
    }
}