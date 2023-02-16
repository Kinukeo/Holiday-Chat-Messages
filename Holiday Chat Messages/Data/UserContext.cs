namespace Holiday_Chat_Messages.Data
{
    public class UserContext
    {
        public string? UserName { get; set; }

        public List<Destination> PossibleDestinations = Destinatons.Data!;

        public QuestionSteps CurrentQuestionStep { get; private set; } = QuestionSteps.Category;

        public bool ProcessCategorySelection(string categoryResponseMessage)
        {
            if (categoryResponseMessage.Contains("lazy", StringComparison.OrdinalIgnoreCase))
            {
                PossibleDestinations = PossibleDestinations.Where(destination => destination.Category == "lazy").ToList();
                return true;
            }
            else if (categoryResponseMessage.Contains("active", StringComparison.OrdinalIgnoreCase))
            {
                PossibleDestinations = PossibleDestinations.Where(destination => destination.Category == "active").ToList();
                return true;
            }

            return false;
        }

        public bool ProcessMaxPriceSelection(string maxPriceResponseMessage)
        {
            if (maxPriceResponseMessage.StartsWith('£'))
                maxPriceResponseMessage = maxPriceResponseMessage.Substring(1);

            bool validParse = double.TryParse(maxPriceResponseMessage, out double maxPrice);
            if (!validParse || maxPrice <= 0)
            {
                return false;
            }

            PossibleDestinations = PossibleDestinations.Where(destination => destination.PricePerPerNight <= maxPrice).ToList();
            return true;
        }

        public bool ProcessResultMessage(string resultMessage)
        {
            if (resultMessage.Contains("try again", StringComparison.OrdinalIgnoreCase))
            {
                PossibleDestinations = Destinatons.Data!;
                return true;
            }

            return false;
        }

        public void MoveToNextQuestionStep()
        {
            switch (CurrentQuestionStep)
            {
                case QuestionSteps.Category:
                    CurrentQuestionStep = QuestionSteps.MaxPrice;
                    break;
                case QuestionSteps.MaxPrice:
                    CurrentQuestionStep = QuestionSteps.Result;
                    break;
                case QuestionSteps.Result: // go back around
                    CurrentQuestionStep = QuestionSteps.Category;
                    break;
            }
        }
    }
}
