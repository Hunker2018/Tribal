using TribalMicroservice.Model;

namespace TribalMicroservice.Repository
{
    public class CreditLineRepository : ICreditLineRepository
    {
        public Response defineRecommendedCreditLine(CreditLine oCreditLine)
        {
            decimal recommendedCreditLine = 0;
            Response response;

            if (oCreditLine.businessType.Equals("SME")) 
            {
                //do Logic here with SME client
                recommendedCreditLine = oCreditLine.monthlyRevenue;
                oCreditLine.recommendedCreditLine = recommendedCreditLine;

            }else if (oCreditLine.businessType.Equals("Startup")) 
            {
                //do Logic here with Startup client
                recommendedCreditLine = Math.Max((oCreditLine.monthlyRevenue / 5), 
                                                 (oCreditLine.cashBalance / 3));

                oCreditLine.recommendedCreditLine = recommendedCreditLine;
            }

            response = new Response
            {
                execute = (oCreditLine.recommendedCreditLine > oCreditLine.requestedCreditLine) ? true : false,
                message = (oCreditLine.recommendedCreditLine > oCreditLine.requestedCreditLine) ?
                                    "Your credit line was accepted and the credit line authorized is " + oCreditLine.recommendedCreditLine :
                                    "Your credit line was rejected"
            };
        

            return response;
        }
    }
}
