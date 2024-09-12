using Microsoft.AspNetCore.Identity;

namespace AcuaParkIdentity.Data
{
    //Para usar estas nuevas propiedades modificar en program.cs y ApplicationDbContext.cs

public class MyRol : IdentityRole
{
    public int idNum { get; set; }
}

}

    /*Volver a generar la migración
    Después de realizar estos cambios, sigue los siguientes pasos nuevamente:

    Elimina la migración anterior:

    bash
    Copiar código
    PM> Remove-Migration
    Crea una nueva migración:

    bash
    Copiar código
    PM> Add-Migration InitialCreate
    Actualiza la base de datos:

    bash
    Copiar código
    PM> Update-Database
    Estos pasos deberían resolver el problema de incompatibilidad del tipo de datos en MySQL y permitir que la migración se aplique correctamente.
    */