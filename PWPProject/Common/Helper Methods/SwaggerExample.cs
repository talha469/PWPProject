using Common.BusinessEntities;
using Swashbuckle.AspNetCore.Filters;

namespace Common.Helper_Methods
{
    public class SwaggerExample : IExamplesProvider<BookMark>
    {
        public BookMark GetExamples()
        {
            return new BookMark()
            {
                Id = "some radnom guid",
                UserId = 1,
                bookMarkDate = DateTime.Now,
                isBookMarked = true

            };
        }
    }
}
