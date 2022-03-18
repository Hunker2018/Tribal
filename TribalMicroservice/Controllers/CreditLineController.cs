using Microsoft.AspNetCore.Mvc;
using TribalMicroservice.Repository;
using TribalMicroservice.Model;
using Microsoft.AspNetCore.Session;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace TribalMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditLineController : ControllerBase
    {
        private readonly ICreditLineRepository _creditLineRepository;
        private readonly ClientRateLimitOptions _options;
        private readonly IClientPolicyStore _clientPolicyStore;

        //private readonly IpRateLimitOptions _options;
        //private readonly IIpPolicyStore _ipPolicyStore;

        public CreditLineController(ICreditLineRepository creditLineRepository, IOptions<ClientRateLimitOptions> optionsAccessor, 
            IClientPolicyStore clientPolicyStore)
        {
            _creditLineRepository = creditLineRepository;
            _options = optionsAccessor.Value;
            _clientPolicyStore = clientPolicyStore;

        }

        [HttpGet]
        public IActionResult Get(CreditLine oCreditLine)
        {
            //HttpContext.Session.Clear();
            Response response;
            int rejectedCount = 0;

            if (HttpContext.Session.GetString("clientRejected") != null)
                rejectedCount = int.Parse(HttpContext.Session.GetString("clientRejected").ToString());

            if (HttpContext.Session.GetString("recommendedCreditLine") != null)
            {
                oCreditLine.recommendedCreditLine = Convert.ToDecimal(HttpContext.Session.GetString("recommendedCreditLine"));

                response = new Response
                {
                    execute = true,
                    message = "Your credit line was accepted and the credit line authorized is " + oCreditLine.recommendedCreditLine
                };
            }
            else
            {
                response = _creditLineRepository.defineRecommendedCreditLine(oCreditLine);
            }

            if (response.execute)
            {
                HttpContext.Session.SetString("recommendedCreditLine", oCreditLine.recommendedCreditLine.ToString());
            }
            else
            {
                rejectedCount++;
                HttpContext.Session.SetString("clientRejected", rejectedCount.ToString());

                if (rejectedCount > 2)
                    response.message = "A sales agent will contact you";
            }
              

            return new OkObjectResult(response.message);

        }

        [HttpPost]
        public async Task Post()
        {

            var id = $"{_options.ClientPolicyPrefix}_cl-key-1";
            var policy = await _clientPolicyStore.GetAsync(id, HttpContext.RequestAborted);

            policy.Rules.Add(new RateLimitRule
            {
                Endpoint = "*/api/CreditLine",
                Period = "30s",
                Limit = 0
            });

            await _clientPolicyStore.SetAsync(id, policy, cancellationToken: HttpContext.RequestAborted);
        }
    }
}
