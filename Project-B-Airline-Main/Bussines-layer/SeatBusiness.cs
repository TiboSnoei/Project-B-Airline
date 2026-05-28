using System;
using System.Collections.Generic;
using System.Linq;

public class SeatLogic
{
    private readonly FlightLogic _flightLogic = new FlightLogic();
    private readonly PlaneLogic _planeLogic = new PlaneLogic();
    private readonly SeatAccess _seatAccess = new SeatAccess();

    public SeatModel[,] GetSeatMapByFlight(FlightModel flight)
    {
        try
        {
            Console.WriteLine(flight.TailNumber);
            PlaneModel plane = _planeLogic.GetPlaneByTailNumber(flight.TailNumber);

            string seatLayout = plane.SeatLayout;

            List<SeatModel> seats = _seatAccess.GetSeatByFlight(flight);

            Dictionary<string, SeatModel> seatLookup = seats
                .ToDictionary(s => s.SeatNumber.Trim().ToUpper());

            string[] rows = seatLayout.Split('|', StringSplitOptions.RemoveEmptyEntries);

            int height = rows.Length;
            int width = rows
                .Max(r => r.Split(',', StringSplitOptions.None).Length);

            SeatModel[,] seatMap = new SeatModel[height, width];

            for (int y = 0; y < rows.Length; y++)
            {
                string[] cells = rows[y].Split(',', StringSplitOptions.None);

                for (int x = 0; x < cells.Length; x++)
                {
                    string token = cells[x]?.Trim();

                    // preserve gaps
                    if (string.IsNullOrEmpty(token) || token.Equals("null", StringComparison.OrdinalIgnoreCase))
                    {
                        seatMap[y, x] = null;
                        continue;
                    }

                    // Extract seat number before "."
                    // "1A.first" -> "1A"
                    string seatNumber = token.Split('.')[0].Trim().ToUpper();

                    if (seatLookup.TryGetValue(seatNumber, out SeatModel seat))
                    {
                        seatMap[y, x] = seat;
                    }
                    else
                    {
                        seatMap[y, x] = null;
                    }
                }
            }

            return seatMap;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}