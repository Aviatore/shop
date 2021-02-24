using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
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

            var publisher1 = new Publisher {PublisherId = 1, PublisherName = "Springer Verlag Berlin and Heidelberg GmbH Co KG"};
            var publisher2 = new Publisher {PublisherId = 2, PublisherName = "Neustar, Inc."};
            var publisher3 = new Publisher {PublisherId = 3, PublisherName = "St Martins Press"};
            var publisher4 = new Publisher {PublisherId = 4, PublisherName = "Penguin Books Ltd"};
            var publisher5 = new Publisher {PublisherId = 5, PublisherName = "Wordsworth Editions Ltd"};
            var publisher6 = new Publisher {PublisherId = 6, PublisherName = "Little Brown Book Group"};
            
            context.Publishers.AddRange( publisher1, publisher2, publisher3, publisher4,
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
                    Description = @"Photographs and stories of 500 women from around the world, based on the author's hugely popular website.
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
                    Description = @"Linus Baker leads a quiet, solitary life. At forty, he lives in a tiny house with a devious cat and his old records. As a Case Worker at the Department in Charge Of Magical Youth, he spends his days overseeing the well-being of children in government-sanctioned orphanages.
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
                    Description = @"'It is philosophy that has the duty of protecting us ... without it no one can lead a life free of fear or worry' 
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
                    Description = @"HARRY DRESDEN IS BACK AND READY FOR ACTION, in the new entry in the #1 New York Times bestselling Dresden Files. 
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
                    Description = @"A magisterial, single-volume history of the greatest conflict the world has ever known by our foremost military historian.
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
                    Description = @"The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter - but that doesn't stop Harry dreaming that he will win the competition. Then at Hallowe'en, when the Goblet of Fire makes its selection, Harry is amazed to find his name is one of those that the magical cup picks out. He will face death-defying tasks, dragons and Dark wizards, but with the help of his best friends, Ron and Hermione, he might just make it through - alive! 
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
                    Description = @"Whenever I hear the word curry, I'm filled with a longing for spicy hot food with the fragrance of cumin, cloves and cinnamon. I see deep red colours from lots of Kashmiri chillis, tinged with a suggestion of yellow from turmeric. I think of the tandoor oven, and slightly scorched naan shining with ghee and garlic. When Indians talk of their food, they talk about their life. To understand this country, you need to understand curry. What makes a good curry? Sensual spicy aromas or thick, creamy sauces? Rich, dark dals or crispy fried street snacks? Rick journeys through India to find the answer, searching this colourful, chaotic nation in search of the truths behind our love affair with its food.",
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
                    Description = @"This new edition of Pro C# 5.0 and the .NET 4.5 Platform has been completely revised and rewritten to reflect the latest changes to the C# language specification and new advances in the .NET Framework. You'll find new chapters covering all the important new features that make .NET 4.5 the most comprehensive release yet, including:
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
                    Description = @"The sixth novel in Julia Quinn's globally beloved and bestselling Bridgerton Family series, set in Regency times and now a series created by Shonda Rhimes for Netflix. Welcome to Francesca's story . . . 
                    In every life there is a turning point. A moment so tremendous, so sharp and breath-taking, that one knows one's life will never be the same. For Michael Stirling, London's most infamous bachelor, that moment came the first time he laid eyes on Francesca Bridgerton.
                    After a lifetime of chasing women, of smiling slyly as they chased him, of allowing himself to be caught but never permitting his heart to become engaged, he took one look at Francesca Bridgerton and fell so fast and hard into love it was a wonder he managed to remain standing.
                    Unfortunately for Michael, however, Francesca's surname was to remain Bridgerton for only a mere thirty-six hours longer - the occasion of their meeting was, lamentably, a supper celebrating her imminent wedding to his cousin . . .",
                    FigUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/3494/9780349429472.jpg",
                    Genre = genre6,
                    Publisher = publisher6
                };
            
            context.Books.AddRange(bookId01, bookId02, bookId03, bookId04, bookId05, bookId06, 
                bookId07, bookId08, bookId09, bookId10, bookId11);

            // List<Book> books = new List<Book>
            // {
            //     bookId01, bookId02, bookId03, bookId04, bookId05, bookId06,
            //     bookId07, bookId08, bookId09, bookId10, bookId11
            // };

            // genre1.Books = books;
            // genre2.Books = books;
            // genre3.Books = books;
            // genre4.Books = books;
            // genre5.Books = books;
            // genre6.Books = books;
            //
            // publisher1.Books = books;
            // publisher2.Books = books;
            // publisher3.Books = books;
            // publisher4.Books = books;
            // publisher5.Books = books;
            // publisher5.Books = books;

            context.SaveChanges();

            return context;
        }
    }
}