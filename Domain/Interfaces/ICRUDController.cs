using Data.Interfaces;
using System.Data;

namespace Domain.Interfaces
{
    internal interface ICRUDController       
    {
        DataTable GetAll();

        DataTable GetById(int id);

        int Edit();

        int DeleteById(int id);
    }
}
