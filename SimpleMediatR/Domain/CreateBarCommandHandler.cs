using System;
using System.Threading.Tasks;

namespace Domain
{
    public class CreateBarCommandHandler : ICommandHandler<CreateBarThatThrowsCommand>
    {
        public async Task Handle(CreateBarThatThrowsCommand message)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            throw new NotImplementedException();
        }
    }
}
