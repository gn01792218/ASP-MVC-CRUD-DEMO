using BulkyBookWep.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWep.Data;

public class DataContext:DbContext
{   //在建構子依賴注入DbContextOptions
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    //創建table
    //這裡的table和Model中的class是對應的
    public DbSet<Category> Categories { get; set; }
}