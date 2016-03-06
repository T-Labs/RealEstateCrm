using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.DAL
{
    public abstract class BaseRepository
    {
        protected ApplicationDbContext DbContext { get; }

#if DEBUG
        private static bool DatabaseInit = false;
#endif

        protected BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
#if DEBUG
            if (!DatabaseInit)
            {
                DatabaseInitData();
                DatabaseInit = true;
            }
#endif
        }
        
#if DEBUG
        private void DatabaseInitData()
        {
            if (!DbContext.Cities.Any())
            {
                DbContext.Cities.Add(new City {Name = "Рязань"});
                DbContext.Cities.Add(new City {Name = "Тула"});

                DbContext.SaveChanges();
            }

            if (!DbContext.TypesHousing.Any())
            {
                var houseTypes = new TypesHousing[]
                {
                    new TypesHousing {Name = "1-к квартира"},
                    new TypesHousing {Name = "2-к квартира"},
                    new TypesHousing {Name = "3-к квартира"},
                    new TypesHousing {Name = "4-к квартира"},
                    new TypesHousing {Name = "5-к квартира"},
                    new TypesHousing {Name = "комната"},
                    new TypesHousing {Name = "коммуналка"}
                };

                foreach (var type in houseTypes)
                {
                    DbContext.TypesHousing.Add(type);
                }

                DbContext.SaveChanges();
            }

            if (!DbContext.Districts.Any())
            {
                var ryazan = DbContext.Cities.First(x => x.Id == 1);
                var tula = DbContext.Cities.First(x => x.Id == 2);
                var districs = new District[]
                    {
                        new District { Name = "Дашково Песочня", City = ryazan },
                         new District { Name = "Дягилево", City = ryazan},
                         new District { Name = "Кальное", City = ryazan},
                         new District { Name = "Канищево", City = ryazan},
                        new District { Name =  "Михайловское шоссе", City = ryazan},
                        new District { Name =  "Московский", City = ryazan},
                         new District { Name = "Недостоево", City = ryazan},
                         new District { Name = "Приокский", City = ryazan},
                        new District { Name =  "Роща", City = ryazan},
                        new District { Name =  "Рыбное", City = ryazan},
                        new District { Name =  "Рязанский р-н", City = ryazan},
                        new District { Name =  "Соколовка", City = ryazan},
                        new District { Name =  "Солотча", City = tula},
                        new District { Name =  "Центр", City = tula},
                        new District { Name =  "Шлаковый", City = tula},
                         new District { Name = "Южный", City = tula},
                         new District { Name = "Центральный р-н", City = tula},
                         new District { Name = "Советский р-н", City = tula},
                         new District { Name = "Привокзальный р-н", City = tula},
                         new District { Name = "Зареченский р-н", City = tula},
                        new District { Name =  "Пролетарский р-н", City = tula},
                        new District { Name =  "Тульская обл", City = tula}
                    };

                foreach (var district in districs)
                {
                    DbContext.Districts.Add(district);
                }

                DbContext.SaveChanges();
            }


            if (!DbContext.Streets.Any())
            {
                #region

                var streets = new Street[]
                {
                    new Street {Name = "1 Мая ул"},
                    new Street {Name = "1 Район ул"},
                    new Street {Name = "1-й Авиационный проезд"},
                    new Street {Name = "1-й Аллейный проезд"},
                    new Street {Name = "1-й Базарный проезд"},
                    new Street {Name = "1-й Дачный пер"},
                    new Street {Name = "1-й Крайний проезд"},
                    new Street {Name = "1-й Озерный пер"},
                    new Street {Name = "1-й Тракторный проезд"},
                    new Street {Name = "10 Линия ул"},
                    new Street {Name = "10 Район ул"},
                    new Street {Name = "11 Линия ул"},
                    new Street {Name = "11 Район ул"},
                    new Street {Name = "12 Линия ул"},
                    new Street {Name = "12 Район ул"},
                    new Street {Name = "14 Линия ул"},
                    new Street {Name = "1905 года пер"},
                    new Street {Name = "2 Бутырки ул"},
                    new Street {Name = "2 Линия ул"},
                    new Street {Name = "2 Район ул"},
                    new Street {Name = "2-й Авиационный проезд"},
                    new Street {Name = "2-й Аллейный проезд"},
                    new Street {Name = "2-й Базарный проезд"},
                    new Street {Name = "2-й Дачный пер"},
                    new Street {Name = "2-й Дягилевский проезд"},
                    new Street {Name = "2-й Крайний проезд"},
                    new Street {Name = "2-й Озерный пер"},
                    new Street {Name = "2-й Тракторный проезд"},
                    new Street {Name = "26 Бакинских Комиссаров пл"},
                    new Street {Name = "3 Бутырки ул"},
                    new Street {Name = "3 Линия ул"},
                    new Street {Name = "3 Район ул"},
                    new Street {Name = "3-й Авиационный проезд"},
                    new Street {Name = "3-й Аллейный проезд"},
                    new Street {Name = "3-й Базарный проезд"},
                    new Street {Name = "3-й Дачный пер"},
                    new Street {Name = "3-й Дягилевский проезд"},
                    new Street {Name = "3-й Озерный пер"},
                    new Street {Name = "3-й Тракторный проезд"},
                    new Street {Name = "3-й Усадебный проезд"},
                    new Street {Name = "4 Линия ул"},
                    new Street {Name = "4 Район ул"},
                    new Street {Name = "4-й Авиационный проезд"},
                    new Street {Name = "4-й Аллейный проезд"},
                    new Street {Name = "4-й Дачный пер"},
                    new Street {Name = "4-й Дягилевский проезд"},
                    new Street {Name = "4-й Озерный пер"},
                    new Street {Name = "4-й Озёрный пер"},
                    new Street {Name = "4-й Тракторный проезд"},
                    new Street {Name = "4-й Усадебный проезд"},
                    new Street {Name = "5 Линия ул"},
                    new Street {Name = "5 Район ул"},
                    new Street {Name = "5-й Авиационный проезд"},
                    new Street {Name = "5-й Аллейный проезд"},
                    new Street {Name = "5-й Дягилевский проезд"},
                    new Street {Name = "5-й Озерный пер"},
                    new Street {Name = "50-летия Октября пл"},
                    new Street {Name = "6 Линия ул"},
                    new Street {Name = "6-й Авиационный проезд"},
                    new Street {Name = "6-й Аллейный проезд"},
                    new Street {Name = "7 Линия ул"},
                    new Street {Name = "7 Район ул"},
                    new Street {Name = "7-й Авиационный проезд"},
                    new Street {Name = "7-й Аллейный проезд"},
                    new Street {Name = "8 Линия ул"},
                    new Street {Name = "8 Марта ул"},
                    new Street {Name = "8 Район ул"},
                    new Street {Name = "8-й Авиационный проезд"},
                    new Street {Name = "9 Линия пер"},
                    new Street {Name = "9 Линия ул"},
                    new Street {Name = "9 Район ул"},
                    new Street {Name = "Авиационная ул"},
                    new Street {Name = "Аллейная 8-й проезд"},
                    new Street {Name = "Аллейная ул"},
                    new Street {Name = "Бабушкина 1-й проезд"},
                    new Street {Name = "Бабушкина ул"},
                    new Street {Name = "Баженова пер"},
                    new Street {Name = "Баженова ул"},
                    new Street {Name = "Базарная ул"},
                    new Street {Name = "Бахмачеевская ул"},
                    new Street {Name = "Безбожная 1-я ул"},
                    new Street {Name = "Безбожная 2-я ул"},
                    new Street {Name = "Белинского проезд"},
                    new Street {Name = "Белинского ул"},
                    new Street {Name = "Белякова ул"},
                    new Street {Name = "Березняковская ул"},
                    new Street {Name = "Березовая ул"},
                    new Street {Name = "Библиотечная ул"},
                    new Street {Name = "Бирюзова ул"},
                    new Street {Name = "Боголюбова ул"},
                    new Street {Name = "Божатково мкр"},
                    new Street {Name = "Божатково п"},
                    new Street {Name = "Больничная (Солотча) ул"},
                    new Street {Name = "Большая (Шереметьево-Песочня) ул"},
                    new Street {Name = "Борки мкр"},
                    new Street {Name = "Братиславская ул"},
                    new Street {Name = "Бронная ул"},
                    new Street {Name = "Бульварный пер"},
                    new Street {Name = "Быстрецкая ул"},
                    new Street {Name = "Вагоны (Соколовка) тер"},
                    new Street {Name = "Введенская ул"},
                    new Street {Name = "Великанова ул"},
                    new Street {Name = "Верхняя ул"},
                    new Street {Name = "Весенняя (Канищево) ул"},
                    new Street {Name = "Весенняя ул"},
                    new Street {Name = "Ветеринарная ул"},
                    new Street {Name = "Вишневая ул"},
                    new Street {Name = "Вишневый (Канищево) пер"},
                    new Street {Name = "Владимирская (Солотча) ул"},
                    new Street {Name = "Вознесенская ул"},
                    new Street {Name = "Войкова пер"},
                    new Street {Name = "Вокзальная ул"},
                    new Street {Name = "Вольная (Солотча) ул"},
                    new Street {Name = "Восточная ул"},
                    new Street {Name = "Восточный промузел мкр"},
                    new Street {Name = "Высоковольтная ул"},
                    new Street {Name = "Гагарина (Соколовка) ул"},
                    new Street {Name = "Гагарина 1-й проезд"},
                    new Street {Name = "Гагарина 2-й проезд"},
                    new Street {Name = "Гагарина 3-й проезд"},
                    new Street {Name = "Гагарина 4-й проезд"},
                    new Street {Name = "Гагарина ул"},
                    new Street {Name = "Газетный пер"},
                    new Street {Name = "Гайдара (Солотча) ул"},
                    new Street {Name = "Гайдара ул"},
                    new Street {Name = "Гаражная ул"},
                    new Street {Name = "Гвардейская ул"},
                    new Street {Name = "Гоголя проезд"},
                    new Street {Name = "Гоголя ул"},
                    new Street {Name = "Голенчинская ул"},
                    new Street {Name = "Голенчинское ш"},
                    new Street {Name = "Горького ул"},
                    new Street {Name = "Гражданская 2-й пер"},
                    new Street {Name = "Гражданская ул"},
                    new Street {Name = "Гражданский 1-й проезд"},
                    new Street {Name = "Грибоедова проезд"},
                    new Street {Name = "Грибоедова ул"},
                    new Street {Name = "Дачная (Канищево) ул"},
                    new Street {Name = "Дачная (Соколовка) ул"},
                    new Street {Name = "Дачная (Солотча) ул"},
                    new Street {Name = "Дачная ул"},
                    new Street {Name = "Дашковская ул"},
                    new Street {Name = "Декабристов проезд"},
                    new Street {Name = "Декабристов ул"},
                    new Street {Name = "Дзержинского ул"},
                    new Street {Name = "Димитрова пл"},
                    new Street {Name = "Димитрова ул"},
                    new Street {Name = "Добролюбова 1-й проезд"},
                    new Street {Name = "Добролюбова 2-й проезд"},
                    new Street {Name = "Добролюбова 3-й проезд"},
                    new Street {Name = "Добролюбова 4-й проезд"},
                    new Street {Name = "Добролюбова 5-й проезд"},
                    new Street {Name = "Добролюбова 6-й проезд"},
                    new Street {Name = "Добролюбова ул"},
                    new Street {Name = "Дорожная (Канищево) ул"},
                    new Street {Name = "Дорожная (Соколовка) ул"},
                    new Street {Name = "Дорожный пер"},
                    new Street {Name = "Достоевского ул"},
                    new Street {Name = "Дружная ул"},
                    new Street {Name = "Дунай (Солотча) ул"},
                    new Street {Name = "Дягилево п"},
                    new Street {Name = "Дягилево ст"},
                    new Street {Name = "Дягилевская ул"},
                    new Street {Name = "Есенина ул"},
                    new Street {Name = "Железнодорожная (Соколовка) ул"},
                    new Street {Name = "Железнодорожная (Солотча) ул"},
                    new Street {Name = "Железнодорожная 1-я ул"},
                    new Street {Name = "Железнодорожная 2-я ул"},
                    new Street {Name = "Животноводческая ул"},
                    new Street {Name = "Животноводческий проезд"},
                    new Street {Name = "Забайкальская ул"},
                    new Street {Name = "Заводская проезд"},
                    new Street {Name = "Завражнова проезд"},
                    new Street {Name = "Загородная ул"},
                    new Street {Name = "Западная ул"},
                    new Street {Name = "Запрудная ул"},
                    new Street {Name = "Заречная ул"},
                    new Street {Name = "Затинная ул"},
                    new Street {Name = "Зафабричная ул"},
                    new Street {Name = "Земляничная ул"},
                    new Street {Name = "Земляничный 1-й проезд"},
                    new Street {Name = "Земляничный 2-й проезд"},
                    new Street {Name = "Земляничный пер"},
                    new Street {Name = "Зубковой ул"},
                    new Street {Name = "Индустриальный 1-й пер"},
                    new Street {Name = "Интернатская ул"},
                    new Street {Name = "Интернациональная ул"},
                    new Street {Name = "К.Маркса ул"},
                    new Street {Name = "Кальная ул"},
                    new Street {Name = "Кальновский туп"},
                    new Street {Name = "Кальное мкр"},
                    new Street {Name = "Кальной проезд"},
                    new Street {Name = "Канищево п"},
                    new Street {Name = "Карцево п"},
                    new Street {Name = "Карцево тер"},
                    new Street {Name = "Карьерная ул"},
                    new Street {Name = "Касимовский пер"},
                    new Street {Name = "Касимовское ш"},
                    new Street {Name = "Качевская ул"},
                    new Street {Name = "Каширина ул"},
                    new Street {Name = "Керамзавода ул"},
                    new Street {Name = "Керамические Выселки ул"},
                    new Street {Name = "Кирпичного завода ул"},
                    new Street {Name = "Коломенская ул"},
                    new Street {Name = "Коломенский 1-й проезд"},
                    new Street {Name = "Коломенский 2-й проезд"},
                    new Street {Name = "Коломенский 3-й проезд"},
                    new Street {Name = "Коломенский 4-й проезд"},
                    new Street {Name = "Коломенский 5-й проезд"},
                    new Street {Name = "Коломенский 6-й проезд"},
                    new Street {Name = "Колхозная (Канищево) ул"},
                    new Street {Name = "Колхозная (Семчино) ул"},
                    new Street {Name = "Колхозная ул"},
                    new Street {Name = "Колхозный проезд"},
                    new Street {Name = "Кольцова ул"},
                    new Street {Name = "Комбайновая ул"},
                    new Street {Name = "Коммунистический пер"},
                    new Street {Name = "Комсомольский пер"},
                    new Street {Name = "Коняева 1-й проезд"},
                    new Street {Name = "Коняева 2-й проезд"},
                    new Street {Name = "Коняева 3-й проезд"},
                    new Street {Name = "Коняева 4-й проезд"},
                    new Street {Name = "Коняева 5-й проезд"},
                    new Street {Name = "Коняева ул"},
                    new Street {Name = "Корнилова ул"},
                    new Street {Name = "Космодемьянской 1-й проезд"},
                    new Street {Name = "Космодемьянской ул"},
                    new Street {Name = "Космонавтов ул"},
                    new Street {Name = "Костычева ул"},
                    new Street {Name = "Котовского проезд"},
                    new Street {Name = "Котовского ул"},
                    new Street {Name = "Крайняя ул"},
                    new Street {Name = "Красная 1-я ул"},
                    new Street {Name = "Красная 2-я ул"},
                    new Street {Name = "Краснорядская ул"},
                    new Street {Name = "Кремлевский вал"},
                    new Street {Name = "Кремль ул"},
                    new Street {Name = "Крупской ул"},
                    new Street {Name = "Кудрявцева ул"},
                    new Street {Name = "Куйбышевское ш"},
                    new Street {Name = "Культуры ул"},
                    new Street {Name = "Кутузова ул"},
                    new Street {Name = "Л.Шевцовой ул"},
                    new Street {Name = "Лагерная ул"},
                    new Street {Name = "Лево-Лыбедска ул"},
                    new Street {Name = "Ленина пл"}
                };


                #endregion
                foreach (var street in streets)
                {
                    DbContext.Streets.Add(street);
                }

                DbContext.SaveChanges();
            }

            if (!DbContext.Objects.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    DbContext.Objects.Add(CreateHousingRadnom());
                    DbContext.SaveChanges();
                }
            }
        }

        private Housing CreateHousingRadnom()
        {
            var houseType = DbContext.TypesHousing.ToList()[Random.Next(0, DbContext.TypesHousing.Count())];
            var city = DbContext.Cities.ToList()[Random.Next(0, DbContext.Cities.Count())]; ;
            var district = DbContext.Districts.ToList()[Random.Next(0, DbContext.Districts.Count())]; ;
            var street = DbContext.Streets.ToList()[Random.Next(0, DbContext.Streets.Count())]; ;

            var sum = Random.Next(5, 30) * 1000;
            Housing item = new Housing()
            {
                City = city,
                Street = street,
                District = district,
                TypesHousing = houseType,
                Comment = Descriptions[Random.Next(0, Descriptions.Length)],
                Sum = sum,
                Phones = new List<HousingPhone> { new HousingPhone { Number = "+123456789" } }
            };
            return item;
        }

        private static readonly Random Random = new Random();

        private static readonly string[] Descriptions = new string[]
          {
                "Сдам квартиру на длительный срок, без посредников, ремонт, стиральная машина, новые батареи(зимой тепло) , унитаз, 2 TV, 4-комфор. плита, газовая колонка(всегда горячая вода), шифоньер, 2 дивана(1 малютка), письменный стол, интернет спаркс, кабельное.",
                "Сдам однокомнатную квартиру в центре на длительный срок,14000 рублей. Индивидуальное отопление,хороший ремонт.Срочно. меблирована. 2 эт. 5 этаж.дома",
                "Сдам квартиру на длительный срок, оплата+счетчики. Звонить с 8-00 до 18-00 в будни.",
                "Сдается 2-х комнатная квартира полностью с мебелью, встроенная кухня с бытовой техникой, телевизором и холодильником , шторы по желанию на длительный срок .",
                "Сдам 2-х комн. квартиру, без посредников, на длительный срок. Косметический ремонт. Частично меблирована, новая кухня, ванная, стиральная машинка, холодильник. 13 000 руб.+коммунал. платежи. Без мебели.",
                "Сдам 1-к квартиру на длительный срок молодой паре или студенткам. Желательно с рязанской пропиской.Квартира очень тёплая, уютная, сделан хороший ремонт, новая сантехника, мебель. Окна пластиковые. 12000р.+ оплата показаний счетчиков воды и эл.энергии.",
                "Сдам 1 комнатную квартиру с мебелью и бытовой техникой на длительный срок порядочной русской семье из 2-х человек, без животных, 14000 т.р .+свет.",
                "Сдам 1 комнатную квартиру ул. Тимуровцев, д. 12/1, рядом с магазинами \"Апельсин\", \"Дикси\", 1/5 этаж панельного дома, 32 кв.м. Есть вся необходимая мебель и бытовая техника, Цена - 11000 руб. + счётчики . На длительный срок.",
                "Сдам комнату Комната 11 м? в 1-к квартире на 5 этаже 5-этажного кирпичного дома Сдам комнату в общежитии. В комнате современный ремонт, пластиковое окно, железная дверь, бойлер. Стоимость 5000+К/У (около 1000р)станко 1-кв, 42/18/13 кв.м., в отличном состоянии, полностью мебелирована, ТВ, стиральная машин., [холодильник, остановка, магазины рядом. 12000 руб. + коммун.услуги.	",
                "Сдам 2-х комнатную квартиру в центе города напротив ТЦ \"Круиз\". В отличном состоянии на длительный срок. Квартира ранее не сдавалась. 20 000 руб + счет.+депозит",
                "Новый дом, хороший ремонт, мебель, бытовая техника, сдача от собственника.Русским, без животных. Можно с детьми. На длит.срок.",
                "Сдам квартиру на длительный срок. Хорошая планировка. Две лоджии. Большая кухня. Удобное расположение, рядом остановка, магазин. без техники. без холодильника. Можно договорится купит технику. Площадь: 56 кв.м.	Этаж 3 из 9	 Тип: Кв.	Комнат: 2	Цена: 14000 руб.	",
                "Сдам 1-комнатную квартиру в Горроще 2/9, мебелирована, холодильник, санузел раздельный, окна пвх, балкон застеклен, телефон, в подъезде домофон. До остановки 2 мин.25.01.2015",
                "Квартира с индивидуальным отоплением в отличном состоянии с мебелью полностью все есть подогрев полов!не риэлтор от хозяина!семье с детьми!оплата 20000 плюс комуслуги(зимой примерно 3500руб летом меньше)",
                "2-к квартира 40 м? на 1 этаже 5-этажного панельного дома, мебелирована, кухня частично.",
                "Сдам 2 комнатную квартиру русской семье на длительный срок +ком.услуги",
                "Сдам двухкомнатную квартиру, на длительный срок с мебелью, холодильником, стиральной машиной. Арендная плата + коммуналка",
                "Cтудентам (курсантам)- не курящим, до 5-ти человек. Имеется стиральная машина автомат, утюг, холодильник, необходимая мебель, посуда. Квартира чистая. На длительный срок. Площадь: 47 кв.м.	Этаж 3 из 3	Тип: Кв.	Комнат: 2	Цена: 18000 руб.	"
          };
#endif
    }

}
