using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmitMapper;
using EmitMapper.MappingConfiguration;
using System.Collections.Generic;

namespace Common.Test
{
    /// <summary>
    /// ConvertUsing     为指定的成员提供自定义的转换逻辑
    /// 
    /// ConvertGeneric   为指定的泛型类型成员提供自定义的转换逻辑
    /// 
    /// ConstructBy      为目标对象使用指定的构造函数替代默认构造函数
    /// 
    /// NullSubstitution 当源对象中指定的成员在为null时，给目标对象的成员赋值
    /// 
    /// IgnoreMembers    忽略指定成员的映射
    /// 
    /// PostProcess      在映射完成后执行指定的方法
    /// 
    /// ShallowMap       指定的成员采用浅拷贝方式映射
    /// 
    /// DeepMap          指定的成员采用深拷贝方式映射
    /// 
    /// MatchMembers    如果成员名称的映射不采用精确匹配，可以指定具体的映射逻辑                
    /// </summary>
    [TestClass]
    public class EmitMapperTest
    {
        #region TestDefaultMapper
        public class Sourse
        {
            public int A;
            public decimal? B;
            public string C;
            public Inner D;
            public string E;
        }

        public class Dest
        {
            public int? A;
            public decimal B;
            public DateTime C;
            public Inner2 D;
            public string F;
        }

        public class Inner
        {
            public long D1;
            public Guid D2;
        }

        public class Inner2
        {
            public long D12;
            public Guid D22;
        }

        [TestMethod]
        public void TestDefaultMapper()
        {
            ObjectsMapper<Sourse, Dest> mapper1 =
               new ObjectMapperManager().GetMapper<Sourse, Dest>(
                   new DefaultMapConfig()
                   .IgnoreMembers<Sourse, Dest>(new string[] { "A" })
                   .NullSubstitution<decimal?, decimal>((value) => -1M)
                   .ConvertUsing<Inner, Inner2>(value => new Inner2 { D12 = value.D1, D22 = value.D2 })
                   .PostProcess<Dest>((value, state) => { value.F = "nothing"; return value; })
                   );

            Sourse src = new Sourse
            {
                C = "2011/9/21 0:00:00",
                D = new Inner
                {
                    D2 = Guid.NewGuid()
                },
                E = "test"
            };


            Dest dst = mapper1.Map(src);

            Assert.IsNull(dst.A);
            Assert.AreEqual<decimal>(dst.B, -1M);
            Assert.AreEqual<Guid>(dst.D.D22, src.D.D2);
            Assert.AreEqual<string>(dst.F, "nothing");
        }
        
        #endregion


        #region 书店MODEL

        /// <summary>
        /// 书店
        /// </summary>
        public class BookStore
        {
            public string Name { get; set; }
            public List<Book> Books { get; set; }
            public Address Address { get; set; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public class Address
        {
            public string Country { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string PostCode { get; set; }
        }

        /// <summary>
        /// 书籍
        /// </summary>
        public class Book
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Language { get; set; }
            public decimal Price { get; set; }
            public List<Author> Authors { get; set; }
            public DateTime? PublishDate { get; set; }
            public Publisher Publisher { get; set; }
            public int? Paperback { get; set; }
        }

        /// <summary>
        /// 发布者
        /// </summary>
        public class Publisher
        {
            public string Name { get; set; }
        }

        /// <summary>
        /// 作者
        /// </summary>
        public class Author
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public ContactInfo ContactInfo { get; set; }
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        public class ContactInfo
        {
            public string Email { get; set; }
            public string Blog { get; set; }
            public string Twitter { get; set; }
        }

        #endregion


        #region 书店DTO

        /// <summary>
        /// 书店DTO
        /// </summary>
        public class BookStoreDto
        {
            public string Name { get; set; }
            public List<BookDto> Books { get; set; }
            public AddressDto Address { get; set; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public class AddressDto
        {
            public string Country1 { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string PostCode { get; set; }
        }

        /// <summary>
        /// 书籍
        /// </summary>
        public class BookDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Language { get; set; }
            public decimal Price { get; set; }
            public DateTime? PublishDate { get; set; }
            public string Publisher { get; set; }
            public int? Paperback { get; set; }
            public string FirstAuthorName { get; set; }
            public string FirstAuthorDescription { get; set; }
            public string FirstAuthorEmail { get; set; }
            public string FirstAuthorBlog { get; set; }
            public string FirstAuthorTwitter { get; set; }
            public string SecondAuthorName { get; set; }
            public string SecondAuthorDescription { get; set; }
            public string SecondAuthorEmail { get; set; }
            public string SecondAuthorBlog { get; set; }
            public string SecondAuthorTwitter { get; set; }
        }

        #endregion


        #region 测试Dto中有null值得数据 字段名不同时需映射

        /// <summary>
        /// 测试Dto中有null值得数据
        /// </summary>
        [TestMethod]
        public void TestAddressNull()
        {
            var mapper = ObjectMapperManager
                   .DefaultInstance
                   .GetMapper<Address, AddressDto>(
                       new DefaultMapConfig()
                           .MatchMembers((x, y) =>
                           {
                               if (x == "Country" && y == "Country1")
                               {
                                   return true;
                               }
                               return x == y;
                           })
                         
                   );

           var addDto=  new Address
            {
                Country = "China",
                City="上海"
            };
           var a = mapper.Map(addDto);
            Assert.IsNotNull(a.City);
            Assert.IsNull(a.Street);
            Assert.IsNull(a.PostCode);
        }
        #endregion


        #region  测试Dto为null 则Model也为null
        /// <summary>
        /// 测试Dto为null 则Model也为null
        /// </summary>
        [TestMethod]
        public void TestDtoNull()
        {
            var mapper = ObjectMapperManager
                    .DefaultInstance
                    .GetMapper<Address, AddressDto>();

            AddressDto address = mapper.Map(null);
            Assert.IsNull(address);
        }

        #endregion


        #region 实现BookStoreDto到BookStore的映射  Book到BookDto映射配置需要以原有List形式配置，
        /// <summary>
        /// 实现BookStoreDto到BookStore的映射
        /// ConvertUsing 加了这个后  则必须配全相应的，否则其他为null
        /// </summary>
        [TestMethod]
        public void TestConvertDtoToModel()
        {
            var mapper = ObjectMapperManager
                  .DefaultInstance
                  .GetMapper<BookStoreDto, BookStore>(
                      new DefaultMapConfig()
                      .ConvertUsing<AddressDto, Address>(s => new Address() { Country = s.Country1 })
                      .ConvertUsing<BookDto, Book>(s => new Book()
                        {
                            PublishDate = s.PublishDate,
                            Publisher = new Publisher() { Name = s.Publisher },
                            Price = s.Price,
                            Authors = new List<Author>() 
                            {  
                                new Author() 
                                {
                                    Name=s.FirstAuthorName, 
                                    Description=s.FirstAuthorDescription,
                                     ContactInfo=new ContactInfo(){ Blog=s.FirstAuthorBlog}
                                },
                                new Author() 
                                {
                                    Name=s.SecondAuthorName, 
                                    Description=s.SecondAuthorDescription,
                                     ContactInfo=new ContactInfo(){ Blog=s.SecondAuthorBlog}
                                }
                            }

                        })
                  );
             BookStoreDto dto = new BookStoreDto
             {
                 Name = "My Store",
                 Address = new AddressDto
                 {
                     Country1="国家",
                     City = "Beijing"
                 },
                 Books = new List<BookDto>  
                                       {  
                                           new BookDto {Title = "RESTful Web Service", FirstAuthorName="第一个作者",  FirstAuthorBlog="blog1", Publisher="发布至"},  
                                           new BookDto {Title = "Ruby for Rails", SecondAuthorName="第二个作者", SecondAuthorBlog="blog2",Price=12}  
                                       }
             };

             BookStore bookStore = mapper.Map(dto);
             Assert.IsNotNull(bookStore.Name);
             Assert.IsNotNull(bookStore.Address.City);
             Assert.IsNotNull(bookStore.Books.Count);
             Assert.IsNotNull(bookStore.Books[0].Authors[0].Name);
             Assert.IsNotNull(bookStore.Books[1].Authors[1].Name);
             Assert.IsNotNull(bookStore.Books[0].Authors[0].ContactInfo.Blog);
             Assert.IsNotNull(bookStore.Books[1].Authors[1].ContactInfo.Blog);

        }



        #endregion
    }
}
