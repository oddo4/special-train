using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using FightBehaviour.Classes;

namespace FightBehaviour
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer BattleTimer = new DispatcherTimer();
        ObservableCollection<Character> orderList = new ObservableCollection<Character>();
        Player p = new Player("Player");
        Monster s = new Monster("Scorpion");

        public MainWindow()
        {
            InitializeComponent();
            orderList.Add(p);
            orderList.Add(s);
            BattleTimer.Interval = new TimeSpan(0,0,1);
            BattleTimer.Tick += TimerAction;
            BattleTimer.Start();

        }

        private void TimerAction(object sender, EventArgs e)
        {
            ValuesP(p);
            ValuesS(s);
            orderList[0].AttackCommand(orderList[0],orderList[1]);
            Character switchC = orderList[0];
            orderList.RemoveAt(0);
            orderList.Add(switchC);
            ValuesP(p);
            ValuesS(s);
            if (p.Stats.HP == 0 || s.Stats.HP == 0)
            {
                BattleTimer.Stop();
            }
        }

        private void ValuesP(Character character)
        {
            labelPlayer.Content = character.Name;
            labelPlayerLives.Content = character.Stats.HP;
            if (pBarPlayer.Maximum == 0)
            {
                pBarPlayer.Maximum = character.Stats.HP;
            }
            pBarPlayer.Value = character.Stats.HP;
            labelPlayerAction.Content = character.AttackBehavior.AttackName;
        }

        private void ValuesS(Character character)
        {
            labelScorpion.Content = character.Name;
            labelScorpionLives.Content = character.Stats.HP;
            if (pBarScorpion.Maximum == 0)
            {
                pBarScorpion.Maximum = character.Stats.HP;
            }
            pBarScorpion.Value = character.Stats.HP;
            labelScorpionAction.Content = character.AttackBehavior.AttackName;
        }
    }
}
