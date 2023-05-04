using BICE.DTO;
using BICE.DAL;
namespace BICE.SRV;

public interface BICE_SRV<Type_DTO> where Type_DTO : BICE_DTO
{
    Type_DTO GetById(int id);
    List<Type_DTO> GetAll();
    Type_DTO Add(Type_DTO dto);
    Type_DTO Update(Type_DTO dto);
    void Delete(Type_DTO dto); 
}