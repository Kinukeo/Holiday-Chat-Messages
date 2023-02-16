using Holiday_Chat_Messages.Data;
using Holiday_Chat_Messages.Models.Requests;

namespace Holiday_Chat_Messages.Services
{
    public class UserMessageService
    {
        private List<UserContext> userContexts = new List<UserContext>();

        private UserContext? GetUserContext(string userName)
        {
            var userContext = userContexts.FirstOrDefault(u => u.UserName == userName);

            if (userContext == null)
            {
                var newUserContext = new UserContext { UserName = userName };
                userContexts.Add(newUserContext);
                userContext = newUserContext;
            }

            return userContext;
        }

        public bool ProcessMessage(UserMessageRequest message)
        {
            var userContext = GetUserContext(message.Sender);
            if (userContext == null)
            {
                return false;
            }

            bool isSuccess = false;

            switch (userContext.CurrentQuestionStep)
            {
                case QuestionSteps.Category:
                    isSuccess = userContext.ProcessCategorySelection(message.Content);
                    break;
                case QuestionSteps.MaxPrice:
                    isSuccess = userContext.ProcessMaxPriceSelection(message.Content);
                    break;
                case QuestionSteps.Result:
                   isSuccess = userContext.ProcessResultMessage(message.Content);
                    break;
            }

            if (isSuccess)
            {
                userContext.MoveToNextQuestionStep();
            }

            return isSuccess;
        }

        public string GetCurrentQuestion(string userName)
        {
            string question = string.Empty;

            var userContext = GetUserContext(userName);
            if (userContext == null)
            {
                return "Unable to find user";
            }

            switch (userContext.CurrentQuestionStep)
            {
                case QuestionSteps.Category:
                    return "Thank you for choosing First Holiday LTD. Would you prefer a lazy or active holiday?";
                 case QuestionSteps.MaxPrice:
                    return "What is the most you would like to spend on your booking? (Pounds Sterling)";
                case QuestionSteps.Result:
                    return userContext.PossibleDestinations.Any() ? $"We have found an option for you! {userContext.PossibleDestinations.MaxBy(destination => destination.PricePerPerNight)}"
                        : "Unable to find a destination within your criteria. To try again, please type 'try again'";
            }

            return "an error has occured";
        }
    }
}
