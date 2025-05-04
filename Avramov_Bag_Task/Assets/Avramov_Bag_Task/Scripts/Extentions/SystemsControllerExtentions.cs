namespace Avramov.Bag
{
    public static class SystemsControllerExtentions
    {
        public static void SetupGameSystems(this SystemsController systemsController)
        {
            systemsController.Deactivate<ChooseBagSystem>();

            systemsController.Activate<GameControlSystem>();
        }

        public static void SetupChooseBagSystems(this SystemsController systemsController)
        {
            systemsController.Activate<ChooseBagSystem>();

            systemsController.Deactivate<GameControlSystem>();
        }
    }
}
