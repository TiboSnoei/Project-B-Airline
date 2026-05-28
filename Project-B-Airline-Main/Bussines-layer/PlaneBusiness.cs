public class PlaneLogic
{
    private PlaneAccess _planeAccess = new PlaneAccess();

    public PlaneModel GetPlaneByTailNumber(string TailNumber)
    {
        return _planeAccess.GetPlaneByTailNumber(TailNumber);
    }
}