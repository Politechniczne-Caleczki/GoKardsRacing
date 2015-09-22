using System;
using System.Collections.Generic;
using System.Text;

namespace GoKardsRacing
{
    class ApplicationStateMenager
    {
        private static ApplicationState state;

        public static ApplicationState State
        {
            get { return state; }
            set
            {
                if (value != state)
                {
                    state = value;
                    ServeState();
                }
            }          
        }

        private static void ServeState()
        {
            switch(state)
            {
                case ApplicationState.Game:
                    {
                        Main.LoadGame();
                    }break;
                case ApplicationState.Menu:
                    {
                        Main.LoadMenu();
                    }break;
            }
        }

        static ApplicationStateMenager()
        {
            state = ApplicationState.Start;
        }
    }
}
