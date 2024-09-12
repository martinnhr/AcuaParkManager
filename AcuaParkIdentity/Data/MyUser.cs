using Microsoft.AspNetCore.Identity;

namespace AcuaParkIdentity.Data
{
    //Para usar estas nuevas propiedades modificar en program.cs y ApplicationDbContext.cs
    
    public class MyUser : IdentityUser
    {
        public int idNum { get; set; }
    }
    
}
