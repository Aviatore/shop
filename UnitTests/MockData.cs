using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using shop.Controllers;
using shop.Models;
using shop.Utilities;

namespace UnitTests
{
    public class MockData
    {
        public static ILogger<HomeController> MoqLogger()
        {
            var logger = new Mock<ILogger<HomeController>>();
            return logger.Object;
        }

        public static IEmailSender MoqEmailSender()
        {
            var emailSender = new Mock<IEmailSender>();
            return emailSender.Object;
        }

        public static IMyLogger MoqMyLogger()
        {
            var myLogger = new Mock<IMyLogger>();
            return myLogger.Object;
        }

        public static shopContext MoqShopContext()
        {
            var moqDatabase = new MockData();
            return moqDatabase.GetTestShopContext();
        }

        public static IEnumerable<OrderedBook> GetRandomMoqOrderedBooksList()
        {
            shopContext context = MoqShopContext();
            Random random = new Random();
            List<int> tmp = new List<int>();
            List<OrderedBook> mockOrderedBooks = new List<OrderedBook>();

            int i = 0;
            while (i < 4)
            {
                int randomBookId = random.Next(1, 12);
                int randomQuantity = random.Next(1, 8);
                tmp.Add(randomBookId);

                if (!tmp.Contains(randomBookId))
                {
                    OrderedBook orderedBook = new OrderedBook
                    {
                        BookId = randomBookId,
                        Quantity = randomQuantity,
                        Title = context.Books.First(x => x.BookId == randomBookId).Title,
                        Price = context.Books.First(x => x.BookId == randomBookId).Price
                    };

                    mockOrderedBooks.Add(orderedBook);

                    i++;
                }
            }

            return mockOrderedBooks;
        }

        /// <summary>
        /// Returns first four Book in List of OrderedBook.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<OrderedBook> GetMoqOrderedBooksList()
        {
            shopContext context = MoqShopContext();
            List<OrderedBook> mockOrderedBooks = new List<OrderedBook>();

            OrderedBook orderedBook1 = new OrderedBook
            {
                BookId = 1,
                Quantity = 3,
                Title = context.Books.First(x => x.BookId == 1).Title,
                Price = context.Books.First(x => x.BookId == 1).Price
            };
            
            OrderedBook orderedBook2 = new OrderedBook
            {
                BookId = 2,
                Quantity = 4,
                Title = context.Books.First(x => x.BookId == 2).Title,
                Price = context.Books.First(x => x.BookId == 2).Price
            };
            
            OrderedBook orderedBook3 = new OrderedBook
            {
                BookId = 3,
                Quantity = 2,
                Title = context.Books.First(x => x.BookId == 3).Title,
                Price = context.Books.First(x => x.BookId == 3).Price
            };
            
            OrderedBook orderedBook4 = new OrderedBook
            {
                BookId = 4,
                Quantity = 1,
                Title = context.Books.First(x => x.BookId == 4).Title,
                Price = context.Books.First(x => x.BookId == 4).Price
            };

            mockOrderedBooks.Add(orderedBook1);
            mockOrderedBooks.Add(orderedBook2);
            mockOrderedBooks.Add(orderedBook3);
            mockOrderedBooks.Add(orderedBook4);

            return mockOrderedBooks;
        }

        public shopContext GetTestShopContext()
        {
            var options = new DbContextOptionsBuilder<shopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new shopContext(options);

            var genre1 = new Genre {GenreId = 1, GenreName = "Computing"};
            var genre2 = new Genre {GenreId = 2, GenreName = "Travel & Holiday Guides"};
            var genre3 = new Genre {GenreId = 3, GenreName = "Fantasy"};
            var genre4 = new Genre {GenreId = 4, GenreName = "Historical"};
            var genre5 = new Genre {GenreId = 5, GenreName = "Horror"};
            var genre6 = new Genre {GenreId = 6, GenreName = "Romance"};

            context.Genres.AddRange(genre1, genre2, genre3, genre4, genre5, genre6);

            var publisher1 = new Publisher
                {PublisherId = 1, PublisherName = "Springer Verlag Berlin and Heidelberg GmbH Co KG"};
            var publisher2 = new Publisher {PublisherId = 2, PublisherName = "Neustar, Inc."};
            var publisher3 = new Publisher {PublisherId = 3, PublisherName = "St Martins Press"};
            var publisher4 = new Publisher {PublisherId = 4, PublisherName = "Penguin Books Ltd"};
            var publisher5 = new Publisher {PublisherId = 5, PublisherName = "Wordsworth Editions Ltd"};
            var publisher6 = new Publisher {PublisherId = 6, PublisherName = "Little Brown Book Group"};

            context.Publishers.AddRange(publisher1, publisher2, publisher3, publisher4,
                publisher5, publisher6);

            var bookId01 = new Book
            {
                BookId = 1,
                Title = "C# (C Sharp Programming) : A Step by Step Guide for Beginners",
                GenreId = 1,
                PublisherId = 5,
                Price = 109.13f,
                Description = @"C# 
                    Have you always wanted to learn c sharp programming language but are afraid it'll be too difficult for you? Or perhaps you know other programming languages but are interested in learning C Sharp language fast?
                    This book is for you. You no longer have to waste your time and money learning C# from boring books that are 600 pages long, expensive online courses or complicated C# tutorials that just leave you more confused. What this book offers... C Sharp for Beginners Complex concepts are broken down into simple steps to ensure that you can easily master the C# language even if you have never coded before. Carefully Chosen C# Examples Examples are carefully chosen to illustrate all concepts. In addition, the output for all examples are provided immediately so you do not have to wait till you have access to your computer to test the examples.",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/6934/9781693465185.jpg",
                Genre = genre1,
                Publisher = publisher5
            };

            var bookId02 = new Book
            {
                BookId = 2,
                Title = "The Atlas of Beauty : Women of the World in 500 Portraits",
                GenreId = 2,
                PublisherId = 4,
                Price = 98.67f,
                Description =
                    @"Photographs and stories of 500 women from around the world, based on the author's hugely popular website.
                    Since 2013 Mihaela Noroc has travelled the world with her backpack and camera taking photos of everyday women to showcase the diversity and beauty all around us. The Atlas of Beauty is a collection of her photographs that celebrates women from fifty countries across the globe and shows that beauty is everywhere, regardless of money, race or social status, and comes in many different sizes and colours. Mihaela's portraits feature women in their native environments, from the Amazon rain forest to markets in India, London city streets and parks in Harlem, creating a mirror of our varied cultures and proving that beauty has no rules.
                    'Stunning . . . aims to challenge the ideals of beauty dictated by the women's fashion magazine industry' Independent",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/8461/9781846149412.jpg",
                Genre = genre2,
                Publisher = publisher4
            };

            var bookId03 = new Book
            {
                BookId = 3,
                Title = "The House in the Cerulean Sea",
                GenreId = 3,
                PublisherId = 3,
                Price = 70.1,
                Description =
                    @"Linus Baker leads a quiet, solitary life. At forty, he lives in a tiny house with a devious cat and his old records. As a Case Worker at the Department in Charge Of Magical Youth, he spends his days overseeing the well-being of children in government-sanctioned orphanages.
                    When Linus is unexpectedly summoned by Extremely Upper Management he's given a curious and highly classified assignment: travel to Marsyas Island Orphanage, where six dangerous children reside: a gnome, a sprite, a wyvern, an unidentifiable green blob, a were-Pomeranian, and the Antichrist. Linus must set aside his fears and determine whether or not they're likely to bring about the end of days.
                    But the children aren't the only secret the island keeps. Their caretaker is the charming and enigmatic Arthur Parnassus, who will do anything to keep his wards safe. As Arthur and Linus grow closer, long-held secrets are exposed, and Linus must make a choice: destroy a home or watch the world burn.",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/2502/9781250217318.jpg",
                Genre = genre3,
                Publisher = publisher3
            };

            var bookId04 = new Book
            {
                BookId = 4,
                Title = "Letters from a Stoic : Epistulae Morales Ad Lucilium",
                GenreId = 4,
                PublisherId = 2,
                Price = 44.67f,
                Description =
                    @"'It is philosophy that has the duty of protecting us ... without it no one can lead a life free of fear or worry' 
                    For several years of his turbulent life, in which he was dogged by ill health, exile and danger, Seneca was the guiding hand of the Roman Empire. This selection of Seneca's letters shows him upholding the ideals of Stoicism - the wisdom of the self-possessed person immune to life's setbacks - while valuing friendship and courage, and criticizing the harsh treatment of slaves and the cruelties in the gladiatorial arena. The humanity and wit revealed in Seneca's interpretation of Stoicism is a moving and inspiring declaration of the dignity of the individual mind. ",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/1404/9780140442106.jpg",
                Genre = genre4,
                Publisher = publisher2
            };

            var bookId05 = new Book
            {
                BookId = 5,
                Title = "The Walking Dead Volume 5: The Best Defense",
                GenreId = 5,
                PublisherId = 1,
                Price = 61.34f,
                Description = @"The world we knew is gone. The world of commerce and 
                    frivolous necessity has been replaced by a world of survival and responsibility. 
                    An epidemic of apocalyptic proportions has swept the globe, causing the dead to 
                    rise and feed on the living. In a matter of months society has crumbled: no 
                    government, no grocery stores, no mail delivery, no cable TV. In a world ruled 
                    by the dead, the survivors are forced to finally start living. 
                    As the survivors settle into their prison home something has drawn them out 
                    into the open... out of the prison... out of their sanctuary. This is a major 
                    turning point for the overall story of The Walking Dead, setting the 
                    stage for years to come. 
                    Reprint Edition",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/5824/9781582406121.jpg",
                Genre = genre5,
                Publisher = publisher1
            };

            var bookId06 = new Book
            {
                BookId = 6,
                Title = "Peace Talks : The Dresden Files, Book Sixteen",
                GenreId = 5,
                PublisherId = 2,
                Price = 39.9,
                Description =
                    @"HARRY DRESDEN IS BACK AND READY FOR ACTION, in the new entry in the #1 New York Times bestselling Dresden Files. 
                    When the Supernatural nations of the world meet up to negotiate an end to ongoing hostilities, Harry Dresden, Chicago's only professional wizard, joins the White Council's security team to make sure the talks stay civil. But can he succeed, when dark political manipulations threaten the very existence of Chicago - and all he holds dear? ",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/3565/9780356515298.jpg",
                Genre = genre5,
                Publisher = publisher2
            };

            var bookId07 = new Book
            {
                BookId = 7,
                Title = "The Second World War",
                GenreId = 4,
                PublisherId = 3,
                Price = 43.57f,
                Description =
                    @"A magisterial, single-volume history of the greatest conflict the world has ever known by our foremost military historian.
                    The Second World War began in August 1939 on the edge of Manchuria and ended there exactly six years later with the Soviet invasion of northern China. The war in Europe appeared completely divorced from the war in the Pacific and China, and yet events on opposite sides of the world had profound effects. Using the most up-to-date scholarship and research, Beevor assembles the whole picture in a gripping narrative that extends from the North Atlantic to the South Pacific and from the snowbound steppe to the North African Desert.
                    Although filling the broadest canvas on a heroic scale, Beevor's The Second World War never loses sight of the fate of the ordinary soldiers and civilians whose lives were crushed by the titanic forces unleashed in this, the most terrible war in history.",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/7802/9781780225647.jpg",
                Genre = genre4,
                Publisher = publisher3
            };

            var bookId08 = new Book
            {
                BookId = 8,
                Title = "Harry Potter and the Goblet of Fire",
                GenreId = 3,
                PublisherId = 3,
                Price = 51.15,
                Description =
                    @"The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter - but that doesn't stop Harry dreaming that he will win the competition. Then at Hallowe'en, when the Goblet of Fire makes its selection, Harry is amazed to find his name is one of those that the magical cup picks out. He will face death-defying tasks, dragons and Dark wizards, but with the help of his best friends, Ron and Hermione, he might just make it through - alive! 
                    These new editions of the classic and internationally bestselling, multi-award-winning series feature instantly pick-up-able new jackets by Jonny Duddle, with huge child appeal, to bring Harry Potter to the next generation of readers. It's time to PASS THE MAGIC ON ... ",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/4088/9781408855683.jpg",
                Genre = genre3,
                Publisher = publisher3
            };

            var bookId09 = new Book
            {
                BookId = 9,
                Title = "Rick Stein's India",
                GenreId = 2,
                PublisherId = 2,
                Price = 78.99f,
                Description =
                    @"Whenever I hear the word curry, I'm filled with a longing for spicy hot food with the fragrance of cumin, cloves and cinnamon. I see deep red colours from lots of Kashmiri chillis, tinged with a suggestion of yellow from turmeric. I think of the tandoor oven, and slightly scorched naan shining with ghee and garlic. When Indians talk of their food, they talk about their life. To understand this country, you need to understand curry. What makes a good curry? Sensual spicy aromas or thick, creamy sauces? Rich, dark dals or crispy fried street snacks? Rick journeys through India to find the answer, searching this colourful, chaotic nation in search of the truths behind our love affair with its food.",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/8499/9781849905787.jpg",
                Genre = genre2,
                Publisher = publisher2
            };

            var bookId10 = new Book
            {
                BookId = 10,
                Title = "Pro C# 5.0 and the .NET 4.5 Framework",
                GenreId = 1,
                PublisherId = 1,
                Price = 93.39,
                Description =
                    @"This new edition of Pro C# 5.0 and the .NET 4.5 Platform has been completely revised and rewritten to reflect the latest changes to the C# language specification and new advances in the .NET Framework. You'll find new chapters covering all the important new features that make .NET 4.5 the most comprehensive release yet, including:
                        .NET APIs for Windows 8 style UI apps 
                    New asynchronous task-based model for async operations 
                    How HTML5 support is being wrapped into C# web applications 
                    New programming interfaces for HTTP applications, including improved IPv6 support 
                    Expanded WPF, WCF and WF libraries giving C# more power than ever before 
                    This comes on top of award winning coverage of core C# features, both old and new, that have made the previous editions of this book so popular (you'll find everything from generics to pLINQ covered here).",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/4302/9781430242338.jpg",
                Genre = genre1,
                Publisher = publisher1
            };

            var bookId11 = new Book
            {
                BookId = 11,
                Title = "Bridgerton: When He Was Wicked: Inspiration for the Netflix Original Series Bridgerton",
                GenreId = 6,
                PublisherId = 6,
                Price = 38.45,
                Description =
                    @"The sixth novel in Julia Quinn's globally beloved and bestselling Bridgerton Family series, set in Regency times and now a series created by Shonda Rhimes for Netflix. Welcome to Francesca's story . . . 
                    In every life there is a turning point. A moment so tremendous, so sharp and breath-taking, that one knows one's life will never be the same. For Michael Stirling, London's most infamous bachelor, that moment came the first time he laid eyes on Francesca Bridgerton.
                    After a lifetime of chasing women, of smiling slyly as they chased him, of allowing himself to be caught but never permitting his heart to become engaged, he took one look at Francesca Bridgerton and fell so fast and hard into love it was a wonder he managed to remain standing.
                    Unfortunately for Michael, however, Francesca's surname was to remain Bridgerton for only a mere thirty-six hours longer - the occasion of their meeting was, lamentably, a supper celebrating her imminent wedding to his cousin . . .",
                FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/3494/9780349429472.jpg",
                Genre = genre6,
                Publisher = publisher6
            };

            context.Books.AddRange(bookId01, bookId02, bookId03, bookId04, bookId05, bookId06,
                bookId07, bookId08, bookId09, bookId10, bookId11);

            var user1 = new User
            {
                UserId = 1,
                UserName = null,
                Email = "brzozasr@gmail.com",
                Phone = null,
                UserAuthId = "6040582f-0c20-44dc-8acc-b684479454b0"
            };
            
            var user2 = new User
            {
                UserId = 2,
                UserName = null,
                Email = "spm1@interia.pl",
                Phone = null,
                UserAuthId = "8befe3cc-024b-4084-ab38-8f5c7663cd1d"
            };
            
            context.Users.AddRange(user1, user2);
            
            var address1 = new Address
            {
                AddressId = 1,
                Street = "Aspekt 20",
                City = "Warsaw",
                Country = "Poland",
                ZipCode = "01-904"
            };
            
            var address2 = new Address
            {
                AddressId = 2,
                Street = "Pl. Zwyciestwa 12",
                City = "Cracow",
                Country = "Poland",
                ZipCode = "12-345"
            };
            
            var address3 = new Address
            {
                AddressId = 3,
                Street = "Sokratesa 15",
                City = "Warsaw",
                Country = "Poland",
                ZipCode = "01-920"
            };
            
            var address4 = new Address
            {
                AddressId = 4,
                Street = "Spacerowa 23",
                City = "Warsaw",
                Country = "Poland",
                ZipCode = "12-678"
            };
            
            var address5 = new Address
            {
                AddressId = 5,
                Street = "Bergen 2",
                City = "Copenhagen",
                Country = "Denmark",
                ZipCode = "345-234"
            };
            
            context.Addresses.AddRange(address1, address2, address3,
                address4, address5);
            
            var order1 = new Order
            {
                OrderId = 1,
                BillingAddressId = 1,
                ShippingAddressId = 1,
                UserId = 1,
                Payment = true,
                Draft = false,
                Date = new DateTime(2021, 2, 18, 16, 32, 15),
                Status = null,
                TotalPrice = 0.00,
                BillingAddress = address1,
                ShippingAddress = address1,
                User = user1
            };
            
            var order2 = new Order
            {
                OrderId = 2,
                BillingAddressId = 2,
                ShippingAddressId = 2,
                UserId = 2,
                Payment = true,
                Draft = false,
                Date = new DateTime(2021, 2, 20, 12, 30, 23),
                Status = null,
                TotalPrice = 0.00,
                BillingAddress = address2,
                ShippingAddress = address2,
                User = user2
            };
            
            var order3 = new Order
            {
                OrderId = 3,
                BillingAddressId = 3,
                ShippingAddressId = 3,
                UserId = 1,
                Payment = true,
                Draft = false,
                Date = new DateTime(2021, 2, 21, 9, 12, 20),
                Status = null,
                TotalPrice = 0.00,
                BillingAddress = address3,
                ShippingAddress = address3,
                User = user1
            };
            
            var order4 = new Order
            {
                OrderId = 4,
                BillingAddressId = 4,
                ShippingAddressId = 5,
                UserId = 2,
                Payment = true,
                Draft = false,
                Date = new DateTime(2021, 2, 25, 16, 35, 59),
                Status = null,
                TotalPrice = 0.00,
                BillingAddress = address4,
                ShippingAddress = address5,
                User = user2
            };
            
            context.Orders.AddRange(order1, order2, order3, order4);
            
            var booksOrdered01 = new BooksOrdered
            {
                BooksOrderedId = 1,
                OrderId = 1,
                BookId = 1,
                Book = bookId01,
                Order = order1
            };
            
            var booksOrdered02 = new BooksOrdered
            {
                BooksOrderedId = 2,
                OrderId = 1,
                BookId = 6,
                Book = bookId06,
                Order = order1
            };
            
            var booksOrdered03 = new BooksOrdered
            {
                BooksOrderedId = 3,
                OrderId = 2,
                BookId = 7,
                Book = bookId07,
                Order = order2
            };
            
            var booksOrdered04 = new BooksOrdered
            {
                BooksOrderedId = 4,
                OrderId = 2,
                BookId = 11,
                Book = bookId11,
                Order = order2
            };
            
            var booksOrdered05 = new BooksOrdered
            {
                BooksOrderedId = 5,
                OrderId = 2,
                BookId = 1,
                Book = bookId01,
                Order = order2
            };
            
            var booksOrdered06 = new BooksOrdered
            {
                BooksOrderedId = 6,
                OrderId = 2,
                BookId = 1,
                Book = bookId01,
                Order = order2
            };
            
            var booksOrdered07 = new BooksOrdered
            {
                BooksOrderedId = 7,
                OrderId = 3,
                BookId = 3,
                Book = bookId03,
                Order = order3
            };
            
            var booksOrdered08 = new BooksOrdered
            {
                BooksOrderedId = 8,
                OrderId = 3,
                BookId = 9,
                Book = bookId09,
                Order = order3
            };
            
            var booksOrdered09 = new BooksOrdered
            {
                BooksOrderedId = 9,
                OrderId = 3,
                BookId = 9,
                Book = bookId09,
                Order = order3
            };
            
            var booksOrdered10 = new BooksOrdered
            {
                BooksOrderedId = 10,
                OrderId = 4,
                BookId = 4,
                Book = bookId04,
                Order = order4
            };
            
            var booksOrdered11 = new BooksOrdered
            {
                BooksOrderedId = 11,
                OrderId = 4,
                BookId = 6,
                Book = bookId06,
                Order = order4
            };
            
            var booksOrdered12 = new BooksOrdered
            {
                BooksOrderedId = 12,
                OrderId = 4,
                BookId = 6,
                Book = bookId06,
                Order = order4
            };
            
            var booksOrdered13 = new BooksOrdered
            {
                BooksOrderedId = 13,
                OrderId = 4,
                BookId = 6,
                Book = bookId06,
                Order = order4
            };
            
            context.BooksOrdereds.AddRange(booksOrdered01, booksOrdered02, booksOrdered03,
                booksOrdered04, booksOrdered05, booksOrdered06, booksOrdered07, booksOrdered08,
                booksOrdered09, booksOrdered10, booksOrdered11, booksOrdered12, booksOrdered13);
            
            var log01 = new Log
            {
                LogId = 1,
                OrderId = 1,
                Msg = "Order data accepted",
                Timestamp = new DateTime(2021, 02, 26, 11, 53, 08),
                Order = order1
            };
            
            var log02 = new Log
            {
                LogId = 2,
                OrderId = 1,
                Msg = "Start payment",
                Timestamp = new DateTime(2021, 02, 26, 11, 53, 30),
                Order = order1
            };
            
            var log03 = new Log
            {
                LogId = 3,
                OrderId = 1,
                Msg = "Payment accepted",
                Timestamp = new DateTime(2021, 02, 26, 11, 53, 45),
                Order = order1
            };
            
            var log04 = new Log
            {
                LogId = 4,
                OrderId = 1,
                Msg = "Sent order confirmation",
                Timestamp = new DateTime(2021, 02, 26, 11, 53, 55),
                Order = order1
            };
            
            var log05 = new Log
            {
                LogId = 5,
                OrderId = 2,
                Msg = "Order data accepted",
                Timestamp = new DateTime(2021, 02, 26, 11, 59, 56),
                Order = order2
            };
            
            var log06 = new Log
            {
                LogId = 6,
                OrderId = 2,
                Msg = "Start payment",
                Timestamp = new DateTime(2021, 02, 26, 12, 00, 5),
                Order = order2
            };
            
            var log07 = new Log
            {
                LogId = 7,
                OrderId = 2,
                Msg = "Payment accepted",
                Timestamp = new DateTime(2021, 02, 26, 12, 00, 28),
                Order = order2
            };
            
            var log08 = new Log
            {
                LogId = 8,
                OrderId = 2,
                Msg = "Sent order confirmation",
                Timestamp = new DateTime(2021, 02, 26, 12, 00, 31),
                Order = order2
            };
            
            var log09 = new Log
            {
                LogId = 9,
                OrderId = 3,
                Msg = "Order data accepted",
                Timestamp = new DateTime(2021, 02, 26, 12, 27, 21),
                Order = order3
            };
            
            var log10 = new Log
            {
                LogId = 10,
                OrderId = 3,
                Msg = "Start payment",
                Timestamp = new DateTime(2021, 02, 26, 12, 27, 38),
                Order = order3
            };
            
            var log11 = new Log
            {
                LogId = 11,
                OrderId = 3,
                Msg = "Payment accepted",
                Timestamp = new DateTime(2021, 02, 26, 12, 28, 44),
                Order = order3
            };
            
            var log12 = new Log
            {
                LogId = 12,
                OrderId = 3,
                Msg = "Sent order confirmation",
                Timestamp = new DateTime(2021, 02, 26, 12, 28, 49),
                Order = order3
            };
            
            var log13 = new Log
            {
                LogId = 13,
                OrderId = 4,
                Msg = "Order data accepted",
                Timestamp = new DateTime(2021, 02, 26, 17, 32, 11),
                Order = order4
            };
            
            var log14 = new Log
            {
                LogId = 14,
                OrderId = 4,
                Msg = "Start payment",
                Timestamp = new DateTime(2021, 02, 26, 17, 32, 27),
                Order = order4
            };
            
            var log15 = new Log
            {
                LogId = 15,
                OrderId = 4,
                Msg = "Payment accepted",
                Timestamp = new DateTime(2021, 02, 26, 17, 32, 51),
                Order = order4
            };
            
            var log16 = new Log
            {
                LogId = 16,
                OrderId = 4,
                Msg = "Sent order confirmation",
                Timestamp = new DateTime(2021, 02, 26, 17, 32, 53),
                Order = order4
            };
            
            context.Logs.AddRange(log01, log02, log03, log04, log05, log06, log07,
                log08, log09, log10, log11, log12, log13, log14, log15, log16);
            
            context.SaveChanges();

            return context;
        }
    }
}