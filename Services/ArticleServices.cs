// using Bloggie.Services.Helper;
// using Bloggie_Services;
// using Entities.Domain;
// using Entities.ViewsModel.Blogs;
// using Microsoft.Extensions.Logging;
// using Repository_Interfaces;
// using ServicesInterfaces;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Articly_Services
// {
//     public class ArticleServices : IBlog
//     {
//         private readonly IBlogRepository _BlogRepository;
//         private readonly ILogger<ArticleServices> _logger;


//         public ArticleServices(IBlogRepository BlogRepository, ILogger<ArticleServices> logger)
//         {
//             _BlogRepository = BlogRepository;
//             _logger = logger;
//         }

//         public async Task<BlogResponse?> AddBlogAsync(AddBlogRequest BlogRequest)
//         {
//             _logger.LogInformation($"Reached To AddBlog() In {this.GetType().Name}");
//             try
//             {
//                 if (BlogRequest == null)
//                     throw new ArgumentNullException(nameof(BlogRequest));



//                 ModelValidate.ModelValidation(BlogRequest);
//             }
//             catch (Exception exception)
//             {
//                 _logger.LogError(exception.Message);
//                 return null;
//             }

//             //List<Guid> Tags = new();

//             //foreach (string tag in BlogRequest.SelectedTags)
//             //{
//             //    Tags.Add(Guid.Parse(tag));
//             //}

//             Blog Blog = BlogRequest.ToBlog();
//             //Blog.
//             Blog.BlogID = Guid.NewGuid();

//             await _BlogRepository.AddAsync(Blog);


//             return Blog.ToResponse();
//         }

//         public async Task<bool> DeleteBlogAsync(Guid id)
//         {
//             _logger.LogInformation($"Reached To DeleteBlogAsync() In {this.GetType().Name}");
//             if (id == Guid.Empty)
//                 return false;

//             if (this.GetBlogAsync(id).Result == null)
//                 return false;

//             return await _BlogRepository.DeleteAsync(id);
//         }

//         public async Task<BlogResponse?> GetBlogAsync(Guid id)
//         {
//             _logger.LogInformation($"Reached To GetBlogById() In {this.GetType().Name}");
//             var Blog = await _BlogRepository.GetByIdAsync(id);
//             return Blog.ToResponse();
//         }


//         public async Task<BlogResponse?> UpdateBlogAsync(UpdateBlogRequest Blog)
//         {
//             Blog? UpdatedBlog;
//             try
//             {
//                 if (Blog == null)
//                     throw new ArgumentNullException(nameof(Blog));

//                 UpdatedBlog = await _BlogRepository.GetByIdAsync(Blog.BlogId);

//                 if (UpdatedBlog == null)
//                     throw new Exception("Can't Find This ID");

//                 ModelValidate.ModelValidation(Blog);
//             }
//             catch (Exception exception)
//             {
//                 _logger.LogError(exception.Message);
//                 return null;
//             }



//             Blog? BlogIsUpdated = await _BlogRepository.UpdateAsync(UpdatedBlog);

//             return BlogIsUpdated?.ToResponse();
//         }
//         public async Task<List<BlogResponse>> GetAllAsync()
//         {
//             _logger.LogInformation("Reached To GetAll In BlogServices");

//             var BlogResponseList = await _BlogRepository.GetAllAsync();

//             return BlogResponseList
//                 .ToList()
//                 .Select(Blog => Blog.ToResponse())
//                 .ToList();
//         }


//     }
// }
