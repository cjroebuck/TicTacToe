using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common;
using NoughtsAndCrosses.Model;
using TicTacToe;

namespace NoughtsAndCrosses
{
	public class MainScreenViewModel:ViewModelBase
	{
        public ObservableCollection<TicTacSquareViewModel> Positions { get; set; }
        public DelegateCommand PlaceMarkCommand { get; private set; }
        public DelegateCommand ResetBoardCommand { get; private set; }
        public DelegateCommand ConnectCommand { get; set; }

        public int Player { get; private set; }
	    private int _counter;
        public string Message { get; set; }

        public MainScreenViewModel()
        {
            Positions = new ObservableCollection<TicTacSquareViewModel>()
                            {
                                new TicTacSquareViewModel(this) {GridPosition = 0},
                                new TicTacSquareViewModel(this) {GridPosition = 1},
                                new TicTacSquareViewModel(this) {GridPosition = 2},
                                new TicTacSquareViewModel(this) {GridPosition = 3},
                                new TicTacSquareViewModel(this) {GridPosition = 4},
                                new TicTacSquareViewModel(this) {GridPosition = 5},
                                new TicTacSquareViewModel(this) {GridPosition = 6},
                                new TicTacSquareViewModel(this) {GridPosition = 7},
                                new TicTacSquareViewModel(this) {GridPosition = 8}
                            };
            PlaceMarkCommand = new DelegateCommand(PlaceMark,t => true);
            ResetBoardCommand = new DelegateCommand(ResetBoard,t=>true);
            ConnectCommand = new DelegateCommand(Connect,t=>true);
            Message = "Player 1's turn...";
        }

        private void Connect(object s)
        {
            MessageBox.Show("Player tries to establish connection...(not implemented!)");
        }

	    private void ResetBoard(object obj)
	    {
	        foreach(var square in Positions)
	        {
	            square.Value = null;
	        }
            
	        _counter = 0;
            Message = "Player 1's turn...";

            OnPropertyChanged("Positions");
            OnPropertyChanged("Message");
	    }

	    public void PlaceMark(object sender)
	    {
            if (!GameEnded())
            {
                var square = sender as TicTacSquareViewModel;
                if (square != null && !square.Value.HasValue)
                {
                    //MessageBox.Show(string.Format("Player ID:{0}, Position:{1}", Player, position));
                    SendMessage(square);
                }
            }
	    }

        // Send Message to server with position and player id
	    private void SendMessage(TicTacSquareViewModel info)
	    {
	        OnReceiveMessage(info);
	    }

        // Receive Callback/Message from server
	    private void OnReceiveMessage(TicTacSquareViewModel info)
	    {
            _counter++;
            Player = _counter % 2;
	        Positions[info.GridPosition].Value = Player.GetCurrentMark();

            if (_counter >= 4 && GameEnded())
            {
                Message = "Player " + (Player + 1) + " wins!";
                // ResetBoard(null);
            }
            else if (_counter >= 9 && !GameEnded())
            {
                Message = "Draw!";
            }
            else
            {
                Message = "Player " + (Player + 1) + "'s turn...";
            }
            OnPropertyChanged("Message");
	    }

        private bool GameEnded()
        {
            var pos = Positions;
            return (pos[0].Value.HasValue && pos[0].Value == pos[4].Value && pos[4].Value == pos[8].Value ||
                    pos[2].Value.HasValue && pos[2].Value == pos[4].Value && pos[4].Value == pos[6].Value ||
                    pos[0].Value.HasValue && pos[0].Value == pos[1].Value && pos[1].Value == pos[2].Value ||
                    pos[3].Value.HasValue && pos[3].Value == pos[4].Value && pos[4].Value == pos[5].Value ||
                    pos[6].Value.HasValue && pos[6].Value == pos[7].Value && pos[7].Value == pos[8].Value ||
                    pos[0].Value.HasValue && pos[0].Value == pos[3].Value && pos[3].Value == pos[6].Value ||
                    pos[1].Value.HasValue && pos[1].Value == pos[4].Value && pos[4].Value == pos[7].Value ||
                    pos[2].Value.HasValue && pos[2].Value == pos[5].Value && pos[5].Value == pos[8].Value);

        }
	}
}