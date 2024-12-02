using Cryptocurrency.Application.ExternalServices;
using Cryptocurrency.Application.ExternalServices.Models;
using Cryptocurrency.Domain.Entities.SymbolEntity;
using Cryptocurrency.Domain.Entities.SymbolEntity.DomainEvents.SymbolEvents;
using Cryptocurrency.Domain.Entities.SymbolEntity.Models;
using Cryptocurrency.Domain.Entities.SymbolEntity.Services;
using Moq;
using Newtonsoft.Json;
namespace Cryptocurrency.Test
{
    public class SymbolTest
    {
        [Fact]
        public void DomainEventTest()
        {


            var domainEvent = new NewSymbolQuoteReceived
            {
                Id = 1930,
                Name = "bitcoin",
                SymbolName = "BTC",
                Quotes = new List<Quote>
                {
                    new() {
                        Name = "USD",
                        LastUpdated = DateTime.UtcNow,
                        MarketCap = 11,
                        Price = 10,
                        FullyDilutedMarketCap = 9,
                        MarketCapDominance = 8,
                        PercentChange_1h = 7,
                        PercentChange_24h = 6,
                        PercentChange_30d = 7,
                        PercentChange_7d = 5,
                        VolumeChange_24h = 4,
                        Volume_24h = 3
                    }
                }
            };
            var symbol = new Symbol(domainEvent);

            Assert.NotNull(symbol);
            Assert.Equal(symbol.Id, domainEvent.Id);
            Assert.Equal(symbol.Name, domainEvent.Name);
            Assert.Equal(symbol.SymbolName, domainEvent.SymbolName);
            Assert.Equal(symbol.Quotes.Count, domainEvent.Quotes.Count);
            Assert.Equal(symbol.Quotes, domainEvent.Quotes);
        }

        [Fact]
        public void SymbolTestWithApiResponse()
        {
            var mockApiService = new Mock<ICryptocurrencyExternalService>();
            mockApiService.Setup(s => s.GetSymbolInfo("BTC").Result).Returns(GetFakeData());

            var api = mockApiService.Object;
            var apiResponse = api.GetSymbolInfo("BTC").Result;
            var firstRecord = apiResponse.Data.FirstOrDefault().Value.FirstOrDefault();
            var domainEvent = new NewSymbolQuoteReceived();
            domainEvent.SymbolName = apiResponse.Data.FirstOrDefault().Key;
            domainEvent.Name = firstRecord.Name;
            domainEvent.Id = firstRecord.Id;
            foreach (var quoteItem in firstRecord.Quote)
            {
                domainEvent.Quotes.Add(new Quote
                {
                    Name = quoteItem.Key,
                    FullyDilutedMarketCap = quoteItem.Value.FullyDilutedMarketCap,
                    LastUpdated = quoteItem.Value.LastUpdated,
                    MarketCap = quoteItem.Value.MarketCap,
                    MarketCapDominance = quoteItem.Value.MarketCapDominance,
                    PercentChange_1h = quoteItem.Value.PercentChange_1h,
                    PercentChange_24h = quoteItem.Value.PercentChange_24h,
                    PercentChange_30d = quoteItem.Value.PercentChange_30d,
                    PercentChange_7d = quoteItem.Value.PercentChange_7d,
                    Price = quoteItem.Value.Price,
                    VolumeChange_24h = quoteItem.Value.VolumeChange_24h,
                    Volume_24h = quoteItem.Value.Volume_24h
                });
            }

            var symbolInfo = new Symbol(domainEvent);
            var mockSymbolRepository = new Mock<ISymbolRepository>();
            mockSymbolRepository.Setup(s => s.Save(symbolInfo)).Returns(Task.CompletedTask);
            mockSymbolRepository.Object.Save(symbolInfo);

            Assert.NotNull(symbolInfo);

        }
        private CryptocurrencyDataModel GetFakeData()
        {
            return JsonConvert.DeserializeObject<CryptocurrencyDataModel>(GetFakeApiResponse());
        }
        private string GetFakeApiResponse()
        {
            return @"{
    ""status"": {
        ""timestamp"": ""2024-12-01T17:45:31.973Z"",
        ""error_code"": 0,
        ""error_message"": null,
        ""elapsed"": 1,
        ""credit_count"": 3,
        ""notice"": null
    },
    ""data"": {
        ""BTC"": [{
            ""id"": 1930,
            ""name"": ""pn91imdz1v"",
            ""symbol"": ""BTC"",
            ""slug"": ""u9kuqokgene"",
            ""is_active"": 5159,
            ""is_fiat"": null,
            ""circulating_supply"": 1025,
            ""total_supply"": 8547,
            ""max_supply"": 5858,
            ""date_added"": ""2024-12-01T17:45:31.972Z"",
            ""num_market_pairs"": 7605,
            ""cmc_rank"": 6510,
            ""last_updated"": ""2024-12-01T17:45:31.972Z"",
            ""tags"": [""jo1qymswkj"", ""z044b8a073"", ""wpzkf4ai9yk"", ""vtopvdvbhl8"", ""wc2xra8ls7"", ""g329bom5m39"", ""q56hl3oyuz"", ""4vbaud65adt"", ""s3cadmqyaki"", ""ianbn84b3b""],
            ""platform"": null,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""quote"": {
                ""EUR"": {
                    ""price"": 0.8295187435659102,
                    ""volume_24h"": 0.03920928960828185,
                    ""volume_change_24h"": 0.5367283035113821,
                    ""percent_change_1h"": 0.4957301605153537,
                    ""percent_change_24h"": 0.5743695965624358,
                    ""percent_change_7d"": 0.9355962287563946,
                    ""percent_change_30d"": 0.36383988041504933,
                    ""market_cap"": 0.10890324845772392,
                    ""market_cap_dominance"": 7745,
                    ""fully_diluted_market_cap"": 0.7155395050188416,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                },
                ""GBP"": {
                    ""price"": 0.8295187435659102,
                    ""volume_24h"": 0.03920928960828185,
                    ""volume_change_24h"": 0.5367283035113821,
                    ""percent_change_1h"": 0.4957301605153537,
                    ""percent_change_24h"": 0.5743695965624358,
                    ""percent_change_7d"": 0.9355962287563946,
                    ""percent_change_30d"": 0.36383988041504933,
                    ""market_cap"": 0.10890324845772392,
                    ""market_cap_dominance"": 7745,
                    ""fully_diluted_market_cap"": 0.7155395050188416,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                },
                ""USD"": {
                    ""price"": 0.8295187435659102,
                    ""volume_24h"": 0.03920928960828185,
                    ""volume_change_24h"": 0.5367283035113821,
                    ""percent_change_1h"": 0.4957301605153537,
                    ""percent_change_24h"": 0.5743695965624358,
                    ""percent_change_7d"": 0.9355962287563946,
                    ""percent_change_30d"": 0.36383988041504933,
                    ""market_cap"": 0.10890324845772392,
                    ""market_cap_dominance"": 7745,
                    ""fully_diluted_market_cap"": 0.7155395050188416,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                }
            }
        }, {
            ""id"": 5915,
            ""name"": ""yrsbwij674"",
            ""symbol"": ""BTC"",
            ""slug"": ""k78eamr3h2"",
            ""is_active"": 5489,
            ""is_fiat"": null,
            ""circulating_supply"": 7855,
            ""total_supply"": 7146,
            ""max_supply"": 5929,
            ""date_added"": ""2024-12-01T17:45:31.972Z"",
            ""num_market_pairs"": 4499,
            ""cmc_rank"": 6033,
            ""last_updated"": ""2024-12-01T17:45:31.972Z"",
            ""tags"": [""ibwjhf5jt0r"", ""b4v00nfkjrp"", ""qxalc0ty34f"", ""hhqzmrur29k"", ""zfsib6ft6x"", ""6neqadawz0q"", ""rtm5b1wae3"", ""mhqtbh2tf9k"", ""nd5q55br7o"", ""uh9f2oo31hc""],
            ""platform"": null,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""quote"": {
                ""EUR"": {
                    ""price"": 0.6686922114873035,
                    ""volume_24h"": 0.15855376190959602,
                    ""volume_change_24h"": 0.826311506420015,
                    ""percent_change_1h"": 0.2782902401552776,
                    ""percent_change_24h"": 0.2438181242281896,
                    ""percent_change_7d"": 0.8877496244811718,
                    ""percent_change_30d"": 0.304922172403834,
                    ""market_cap"": 0.18991906839472672,
                    ""market_cap_dominance"": 3707,
                    ""fully_diluted_market_cap"": 0.9637267108997305,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                },
                ""GBP"": {
                    ""price"": 0.6686922114873035,
                    ""volume_24h"": 0.15855376190959602,
                    ""volume_change_24h"": 0.826311506420015,
                    ""percent_change_1h"": 0.2782902401552776,
                    ""percent_change_24h"": 0.2438181242281896,
                    ""percent_change_7d"": 0.8877496244811718,
                    ""percent_change_30d"": 0.304922172403834,
                    ""market_cap"": 0.18991906839472672,
                    ""market_cap_dominance"": 3707,
                    ""fully_diluted_market_cap"": 0.9637267108997305,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                },
                ""USD"": {
                    ""price"": 0.6686922114873035,
                    ""volume_24h"": 0.15855376190959602,
                    ""volume_change_24h"": 0.826311506420015,
                    ""percent_change_1h"": 0.2782902401552776,
                    ""percent_change_24h"": 0.2438181242281896,
                    ""percent_change_7d"": 0.8877496244811718,
                    ""percent_change_30d"": 0.304922172403834,
                    ""market_cap"": 0.18991906839472672,
                    ""market_cap_dominance"": 3707,
                    ""fully_diluted_market_cap"": 0.9637267108997305,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                }
            }
        }, {
            ""id"": 1429,
            ""name"": ""um5h20ksny"",
            ""symbol"": ""BTC"",
            ""slug"": ""7syu4thm8ym"",
            ""is_active"": 6240,
            ""is_fiat"": null,
            ""circulating_supply"": 542,
            ""total_supply"": 3879,
            ""max_supply"": 2604,
            ""date_added"": ""2024-12-01T17:45:31.972Z"",
            ""num_market_pairs"": 4623,
            ""cmc_rank"": 3020,
            ""last_updated"": ""2024-12-01T17:45:31.972Z"",
            ""tags"": [""6mf54dxl2c8"", ""zr6f86f9y0e"", ""hq2r14obrae"", ""sxtix2kl1yk"", ""x2xc2vhwkh"", ""xmp69rgucik"", ""a9937g5ni9c"", ""68kqs0vgp7b"", ""bi1ngps83s9"", ""05g5gmf61ybn""],
            ""platform"": null,
            ""self_reported_circulating_supply"": null,
            ""self_reported_market_cap"": null,
            ""quote"": {
                ""EUR"": {
                    ""price"": 0.9289259598969544,
                    ""volume_24h"": 0.7959256354300777,
                    ""volume_change_24h"": 0.5496338208813634,
                    ""percent_change_1h"": 0.9185973016357696,
                    ""percent_change_24h"": 0.9591179574291491,
                    ""percent_change_7d"": 0.1497379806619774,
                    ""percent_change_30d"": 0.6285869419634786,
                    ""market_cap"": 0.8276513020366514,
                    ""market_cap_dominance"": 9408,
                    ""fully_diluted_market_cap"": 0.12554037716095956,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                },
                ""GBP"": {
                    ""price"": 0.9289259598969544,
                    ""volume_24h"": 0.7959256354300777,
                    ""volume_change_24h"": 0.5496338208813634,
                    ""percent_change_1h"": 0.9185973016357696,
                    ""percent_change_24h"": 0.9591179574291491,
                    ""percent_change_7d"": 0.1497379806619774,
                    ""percent_change_30d"": 0.6285869419634786,
                    ""market_cap"": 0.8276513020366514,
                    ""market_cap_dominance"": 9408,
                    ""fully_diluted_market_cap"": 0.12554037716095956,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                },
                ""USD"": {
                    ""price"": 0.9289259598969544,
                    ""volume_24h"": 0.7959256354300777,
                    ""volume_change_24h"": 0.5496338208813634,
                    ""percent_change_1h"": 0.9185973016357696,
                    ""percent_change_24h"": 0.9591179574291491,
                    ""percent_change_7d"": 0.1497379806619774,
                    ""percent_change_30d"": 0.6285869419634786,
                    ""market_cap"": 0.8276513020366514,
                    ""market_cap_dominance"": 9408,
                    ""fully_diluted_market_cap"": 0.12554037716095956,
                    ""last_updated"": ""2024-12-01T17:45:31.972Z""
                }
            }
        }]
    }
}";
        }
    }
}