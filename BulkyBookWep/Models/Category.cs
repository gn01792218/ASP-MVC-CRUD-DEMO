using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BulkyBookWep.Models;
public class Category
{   //data annotation
    //由於這些對應的是資料庫的column，我們若想要為每個column設置config，就得寫sql
    //使用data annotation 就可以不用寫sql語言囉!
    //data annotation 用 []來撰寫，有哪些可以使用的請查詢官網的DataAnnotations Namespace
    
    [Key]  //寫成sql的primary key 並且是identity column
    public int Id { get; set; }  
    
    [Required]  //設置為not null
    [DisplayName("姓名")] //假如沒有設置DisplayName，將直接以該屬性名稱Name作為<label asp-for="Name"></label>的顯示
    public string Name { get; set; }
    
    [DisplayName("顯示順序")] //假如沒有設置DisplayName，將直接以該屬性名稱Name作為<label asp-for="Name"></label>的顯示
    [Range(1,100,ErrorMessage = "不可以輸入超出1~100的範圍唷!")] //設置Range範圍
    public int DisplayOrder { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
