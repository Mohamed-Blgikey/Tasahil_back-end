using AutoMapper;
using Tasahil_.net_core_6_.Entity;
using Tasahil_.net_core_6_.Models;

namespace Tasahil_.net_core_6_.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            {
                CreateMap<CategoryVM, Category>();
                CreateMap<Category, CategoryVM>();


                CreateMap<Post, PostVM>();
                CreateMap<PostVM, Post>();

                CreateMap<Comment, CommentVM>();
                CreateMap<CommentVM, Comment>();
            }


        }
    }
}
