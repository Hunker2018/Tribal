using TribalMicroservice.Model; 
namespace TribalMicroservice.Repository
{
    public interface ICreditLineRepository
    {
        Response defineRecommendedCreditLine(CreditLine oCreditLine);
    }
}
