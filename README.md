# Tribal
A short and incomplete backend test
This is the final result of the backend test. The .NET version used is 6.0, first time using it, so it was a really nice experience (I normally use .NET Core 3.0 or 
.NET framework 4.5)

I managed to control the client/ip request for the microservice, but unfortunately couldn't control it with different times and request, however the code to modify it
at runtime is there (Post method in CreditLineController). For this I used ASPNETCORERATELIMIT: https://github.com/stefanprodan/AspNetCoreRateLimit
You can run this API in POSTMAN with the GET method: /api/CreditLine

The body example is:
{
    "businessType": "SME",
    "cashBalance": 4500.30,
    "monthlyRevenue": 4500.45,
    "requestedCreditLine": 4800,
    "requestedDate": "2022-03-15T02:03:57.066Z",
    "recommendedCreditLine": 0
}

HOPE YOU HAVING A NICE DAY.
