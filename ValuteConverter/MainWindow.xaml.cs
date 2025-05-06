using System.Windows;
using ValuteConverter.AppData;

namespace ValuteConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CourseService _courseService;
        public MainWindow()
        {
            InitializeComponent();
            _courseService = new CourseService(SellValuteCmb, BuyValuteCbm, SellAmountTb, BuyAmountTb, SellRatioTbl, BuyRatioTbl, UpdateDateTbl);

            _courseService.LoadCource();
        }
    }
}
