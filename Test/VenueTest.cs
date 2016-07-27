using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;


namespace bandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Venue.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Venue firstVenue = new Venue("Roxy");
      Venue secondVenue = new Venue("Roxy");

      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Test_Save_SavesVenueToDatabase()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();

      List<Venue> allVenues = Venue.GetAll();
      List<Venue> result = new List<Venue>{testVenue};

      Assert.Equal(result, allVenues);

    }
    [Fact]
    public void Test_Save_AssignsIdtoVenueObject()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];
      int result = savedVenue.GetId();
      int testResult = testVenue.GetId();

      Assert.Equal(testResult, result);

    }
    [Fact]
    public void Test_Find_FindVenueInDatabase()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();
      Venue foundVenue = Venue.Find(testVenue.GetId());
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
    public void Test_GetBands_GetsAllBandsThatPlayedAtThisVenue()
    {
      Venue testVenue = new Venue("Roxy");
      testVenue.Save();

      Band test1Band = new Band("Queen");
      test1Band.Save();

      Band test2Band = new Band("The Police");
      test2Band.Save();
      testVenue.AddBand(test1Band);

      List<Band> result = testVenue.GetBands();

      List<Band> expectedResult = new List<Band> {test1Band};

      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Test_Update_UpdatesVenueInDatabase()
    {
      //Arrange
      string name = "Roxy";
      Venue testVenue = new Venue(name);
      testVenue.Save();
      string newName = "The Greek";

      //Act
      testVenue.Update(newName);

      string result = testVenue.GetName();

      //Assert
      Assert.Equal(newName, result);
    }
    [Fact]
    public void Test_Delete_DeletesVenuesFromDatabase()
    {
      //Arrange
      string name1 = "Roxy";
      Venue testVenue1 = new Venue(name1);
      testVenue1.Save();

      string name2 = "The Greek";
      Venue testVenue2 = new Venue(name2);
      testVenue2.Save();

      //Act
      testVenue1.Delete();
      List<Venue> resultVenues = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue> {testVenue2};

      //Assert
      Assert.Equal(testVenueList, resultVenues);
    }
    [Fact]
    public void Test_AddBand_AddsBandToVenue()
    {
      Venue testVenue = new Venue("The Greek");
      testVenue.Save();
      Band testBand = new Band("Queen");
      testBand.Save();
      Band testBand2 = new Band("The Police");
      testBand2.Save();
      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);
      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_GetBands_ReturnsAllVenueBands()
    {
      //Arrange
      Venue testVenue = new Venue("The Greek");
      testVenue.Save();

      Band testBand1 = new Band("Queen");
      testBand1.Save();

      Band testBand2 = new Band("The Police");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand1);
      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1};

      //Assert
      Assert.Equal(testList, savedBands);
    }
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
  }
}
