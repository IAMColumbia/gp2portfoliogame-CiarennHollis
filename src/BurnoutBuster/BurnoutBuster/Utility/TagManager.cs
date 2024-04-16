namespace BurnoutBuster.Utility
{
    public class TagManager
    {
        // P R O P E R T I E S
        private static TagManager instance;
        public static TagManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TagManager();
                return instance;
            }
        }

        // C O N S T R U C T O R 
        protected TagManager()
        {

        }

        // M E T H O D S
        public static bool CompareTag(ITaggable target, Tags tag)
        {
            if (target.Tag == tag) 
                return true;

            return false;
        }
    }

    public enum Tags { None, Player, Enemy, Weapon, Health }
}
