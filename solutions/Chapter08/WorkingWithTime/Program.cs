using static System.Console;

WriteLine("Earliest date/time value is: {0}", DateTime.MinValue);
WriteLine("UNIX epoch date/time value is: {0}", DateTime.UnixEpoch);
WriteLine("Date/time value Now is: {0}", DateTime.Now);
WriteLine("Date/time value Today is: {0}", DateTime.Today);

DateTime christmas = new(year: 2023, month: 12, day: 25);
WriteLine("Christmas: {0}",
  christmas); // default format
WriteLine("Christmas: {0:dddd, dd MMMM yyyy}",
  christmas); // custom format
WriteLine("Christmas is in month {0} of the year.",
  christmas.Month);
WriteLine("Christmas is day {0} of the year.",
  christmas.DayOfYear);
WriteLine("Christmas {0} is on a {1}.",
  christmas.Year,
  christmas.DayOfWeek);

DateTime beforeXmas = christmas.Subtract(TimeSpan.FromDays(12));
DateTime afterXmas = christmas.AddDays(12);
WriteLine("12 days before Christmas is: {0}",
  beforeXmas);
WriteLine("12 days after Christmas is: {0}",
  afterXmas);
TimeSpan untilChristmas = christmas - DateTime.Now;
WriteLine("There are {0} days and {1} hours until Christmas.",
  untilChristmas.Days,
  untilChristmas.Hours);
WriteLine("There are {0:N0} hours until Christmas.",
  untilChristmas.TotalHours);

DateTime kidsWakeUp = new(
  year: 2021, month: 12, day: 25,
  hour: 6, minute: 30, second: 0);
WriteLine("Kids wake up on Christmas: {0}",
  kidsWakeUp);
WriteLine("The kids woke me up at {0}",
  kidsWakeUp.ToShortTimeString());