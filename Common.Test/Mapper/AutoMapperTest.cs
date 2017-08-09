using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AutoMapper;
using System.Reflection;

namespace Common.Test
{
    [TestClass]
    public class AutoMapperTest
    {

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


        #region 测试CreateMap
        /// <summary>
        /// 测试CreateMap
        /// </summary>
        [TestMethod]
        public void TestCreateMap()
        {
            var exp = Mapper.CreateMap<AddressDto, Address>();
            exp.ForMember(add => add.Country, opt => opt.MapFrom(adty => adty.Country1));
            AddressDto dto = new AddressDto
            {
                Country1 = "China",
                City = "Beijing",
                Street = "Dongzhimen Street",
                PostCode = "100001"
            };
            Address address = Mapper.Map<AddressDto, Address>(dto);
            address.Country.Equals("China");
            address.City.Equals("Beijing");
            address.Street.Equals("Dongzhimen Street");
            address.PostCode.Equals("100001");
            // Assert.Equals(address, dto);
        }
        #endregion


        #region 测试Dto中有null值得数据

        /// <summary>
        /// 测试Dto中有null值得数据
        /// </summary>
        [TestMethod]
        public void TestAddressNull()
        {
            Mapper.CreateMap<AddressDto, Address>();

            Address address = Mapper.Map<AddressDto, Address>(new AddressDto
            {
                Country1 = "China"
            });
            Assert.IsNull(address.City);
            Assert.IsNull(address.Street);
            Assert.IsNull(address.PostCode);
        }
        #endregion


        #region  测试Dto为null 则Model也为null
        /// <summary>
        /// 测试Dto为null 则Model也为null
        /// </summary>

        [TestMethod]
        public void TestDtoNull()
        {
            Mapper.CreateMap<AddressDto, Address>();

            Address address = Mapper.Map<AddressDto, Address>(null);
            Assert.IsNull(address);
        }

        #endregion


        #region 实现BookStoreDto到BookStore的映射  每个属性单独配置
        /// <summary>
        /// 实现BookStoreDto到BookStore的映射
        /// </summary>
        [TestMethod]
        public void TestConvertDtoToModel()
        {
            var exp = Mapper.CreateMap<BookDto, Book>();
          
            exp.ForMember(bok => bok.Authors,
                (opt) => opt.MapFrom(bookdto =>
                        new List<Author>() { new Author()
                        {
                            Name = bookdto.FirstAuthorName,
                            Description = bookdto.FirstAuthorDescription,
                            ContactInfo = new ContactInfo() { Blog = bookdto.FirstAuthorBlog, Email = bookdto.FirstAuthorEmail, Twitter = bookdto.FirstAuthorTwitter }
                        },new Author()
                        {
                            Name = bookdto.SecondAuthorName,
                            Description = bookdto.SecondAuthorDescription,
                            ContactInfo = new ContactInfo() { Blog = bookdto.SecondAuthorBlog, Email = bookdto.SecondAuthorEmail, Twitter = bookdto.SecondAuthorTwitter }
                        }}
                    ));

            exp.ForMember(bok => bok.Publisher/*(变量)*/,
                      (map) => map.MapFrom(bookdto => new Publisher() { Name = bookdto.Publisher/*(DTO的变量)*/}));

            //有一一对应关系的可以不用再添加
            //exp.ForMember(d => d.Description/*变量*/, opt => opt.MapFrom(s => s.Description)/*DTO的变量*/);
            //exp.ForMember(d => d.Language, opt => opt.MapFrom(s => s.Language));
            //exp.ForMember(d => d.Paperback, opt => opt.MapFrom(s => s.Paperback));
            //exp.ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price));
            //exp.ForMember(d => d.PublishDate, opt => opt.MapFrom(s => s.PublishDate));
            // exp.ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title));
            
            

            //该方法主要用来检查还有那些规则没有写完。
            Mapper.AssertConfigurationIsValid();

            Mapper.CreateMap<AddressDto, Address>();
            Mapper.CreateMap<BookStoreDto, BookStore>();
            BookStoreDto dto = new BookStoreDto
            {
                Name = "My Store",
                Address = new AddressDto
                {
                    City = "Beijing"
                },
                Books = new List<BookDto>  
                                       {  
                                           new BookDto {Title = "RESTful Web Service", FirstAuthorName="第一个作者",  FirstAuthorBlog="blog1"},  
                                           new BookDto {Title = "Ruby for Rails", SecondAuthorName="第二个作者", SecondAuthorBlog="blog2"}  
                                       }
            };
            BookStore bookStore = Mapper.Map<BookStoreDto, BookStore>(dto);
            Assert.IsNotNull(bookStore.Name);
            Assert.IsNotNull(bookStore.Address.City);
            Assert.IsNotNull(bookStore.Books.Count);
            Assert.IsNotNull(bookStore.Books[0].Authors[0].Name);
            Assert.IsNotNull(bookStore.Books[1].Authors[1].Name);
            Assert.IsNotNull(bookStore.Books[0].Authors[0].ContactInfo.Blog);
            Assert.IsNotNull(bookStore.Books[1].Authors[1].ContactInfo.Blog);


        }
        #endregion


        #region 实现BookStore到BookStoreDto的映射
        /// <summary>
        /// 实现BookStore到BookStoreDto的映射
        /// </summary>
        [TestMethod]
        public void TestConvertModelToDTO()
        {
            var exp = Mapper.CreateMap<Book, BookDto>();
            // exp.ForMember(dto => dto.Description/*(Dto变量)*/,opt=>opt.MapFrom(m=>m.Description) /*(的变量)*/);
            exp.ForMember(dto => dto.FirstAuthorBlog, opt => opt.MapFrom(m => m.Authors[0].ContactInfo.Blog));
            exp.ForMember(dto => dto.FirstAuthorDescription, opt => opt.MapFrom(m => m.Authors[0].Description));
            exp.ForMember(dto => dto.FirstAuthorEmail, opt => opt.MapFrom(m => m.Authors[0].ContactInfo.Email));
            exp.ForMember(dto => dto.FirstAuthorName, opt => opt.MapFrom(m => m.Authors[0].Name));
            exp.ForMember(dto => dto.FirstAuthorTwitter, opt => opt.MapFrom(m => m.Authors[0].ContactInfo.Twitter));

            exp.ForMember(dto => dto.SecondAuthorBlog, opt => opt.MapFrom(m => m.Authors[1].ContactInfo.Blog));
            exp.ForMember(dto => dto.SecondAuthorDescription, opt => opt.MapFrom(m => m.Authors[1].Description));
            exp.ForMember(dto => dto.SecondAuthorEmail, opt => opt.MapFrom(m => m.Authors[1].ContactInfo.Email));
            exp.ForMember(dto => dto.SecondAuthorName, opt => opt.MapFrom(m => m.Authors[1].Name));
            exp.ForMember(dto => dto.SecondAuthorTwitter, opt => opt.MapFrom(m => m.Authors[1].ContactInfo.Twitter));

            exp.ForMember(dto => dto.Publisher, opt => opt.MapFrom(m => m.Publisher.Name));

            //该方法主要用来检查还有那些规则没有写完。
            Mapper.AssertConfigurationIsValid();

            //涉及到的关系映射一个都不能缺少，否则会报错
            Mapper.CreateMap<BookStore, BookStoreDto>();
            Mapper.CreateMap<Address, AddressDto>();
            #region BookStore的Model定义
            BookStore bookstore = new BookStore()
              {
                  Name = "书店名",
                  Address = new Address() { City = "城市", Country = "国家", PostCode = "邮局编号", Street = "街道" },
                  Books = new List<Book>() {
                              new Book(){ 
                                     Description="one描述", 
                                     Language="one语言", 
                                     Paperback=1,
                                     Price=12,
                                     PublishDate=DateTime.Now, 
                                     Publisher=new Publisher()
                                     { 
                                         Name="one出版社名"
                                     },
                                     Title="标题one",
                                      Authors=new List<Author>()
                                      { 
                                          new Author(){
                                              Name="作者名1",
                                              Description="描述", 
                                              ContactInfo=new ContactInfo()
                                              {
                                                  Blog="博客", Twitter="twitter1", Email="Email1"
                                              }
                                          },
                                          new Author(){
                                              Name="作者名1",
                                              Description="描述", 
                                              ContactInfo=new ContactInfo()
                                              {
                                                  Blog="博客", Twitter="twitter1", Email="Email1"
                                              }
                                          }
  
                                      }  
                               }
                }
              };
            #endregion

            BookStoreDto bookdto = Mapper.Map<BookStore, BookStoreDto>(bookstore);
            Assert.IsNotNull(bookdto.Address.City);
            Assert.IsNotNull(bookdto.Books[0].Description);
            Assert.IsNotNull(bookdto.Books[1].FirstAuthorBlog);
            Assert.IsNotNull(bookdto.Books[1].FirstAuthorName);
            Assert.IsNotNull(bookdto.Name);

        } 
        #endregion


        #region BookDto到Publisher的映射
        /// <summary>
        /// BookDto到Publisher的映射
        /// </summary>
        [TestMethod]
        public void ConvertBookDtoToPublisher()
        {
            var map = Mapper.CreateMap<BookDto,Publisher>();  
            map.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Publisher));
           

            BookDto dto = new BookDto() {  Publisher="出版者"};

            Publisher publish = Mapper.Map<BookDto, Publisher>(dto);
            Assert.IsNotNull(publish.Name);
        } 
        #endregion

        #region BookDto到ContactInfo的映射  
        /// <summary>
        /// BookDto到ContactInfo的映射
        /// 使用ConstructUsing的方式一次直接定义好所有字段的映射规则
        /// </summary>
        [TestMethod]
        public void ConvertBookDtoToContactInfo()
        {
            var map = Mapper.CreateMap<BookDto, ContactInfo>();
            map.ConstructUsing(s => new ContactInfo
            {
                Blog = s.FirstAuthorBlog,
                Email = s.FirstAuthorEmail,
                Twitter = s.FirstAuthorTwitter
            });

            BookDto dto = new BookDto
            {
                FirstAuthorEmail = "matt.rogen@abc.com",
                FirstAuthorBlog = "matt.amazon.com",
            };
            ContactInfo contactInfo = Mapper.Map<BookDto, ContactInfo>(dto);
        } 
        #endregion


        #region test AutoMapper custom type convert using ConstructServicesUsing
        /// <summary>
        /// 测试String->Int,string->DateTime,string->Type
        /// </summary>
        [TestMethod]
        public void TestConvertStringToInt()
        {
            Mapper.CreateMap<string, int>().ConvertUsing(new Int32TypeConverter());
            Mapper.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            Mapper.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
            Mapper.CreateMap<Source, Destination>();
            Mapper.AssertConfigurationIsValid();

            var source = new Source
            {
                Value1 = "5",
                Value2 = "01/01/2000",
                Value3 = "AutoMapperSamples.GlobalTypeConverters.GlobalTypeConverters+Destination"
            };

            Destination result = Mapper.Map<Source, Destination>(source);
            result.Value3.Equals(typeof(Destination));

        }


        public class Int32TypeConverter : ITypeConverter<string, Int32>
        {
            public int Convert(ResolutionContext context)
            {

                return System.Convert.ToInt32(context.SourceValue);
            }
        }
        public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
        {

            public DateTime Convert(ResolutionContext context)
            {
                return System.Convert.ToDateTime(context.SourceValue);
            }
        }

        public class TypeTypeConverter : ITypeConverter<string, Type>
        {

            public Type Convert(ResolutionContext context)
            {
                return context.SourceType;
            }
        }


        public class Source
        {
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
        }

        public class Destination
        {
            public int Value1 { get; set; }
            public DateTime Value2 { get; set; }
            public Type Value3 { get; set; }
        } 
        #endregion

    
    
    }
}
