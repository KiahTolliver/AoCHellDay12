namespace AoCDay12Tests;

public class Tests
{

    private RecordParser sut;

    [SetUp]
    public void Setup()
    {
        sut = new RecordParser();
    }

    [Test]
    public void Verify_IsValid() 
    {
        string[] input = """
            #.#.### 1,1,3
            .#...#....###. 1,1,3
            .#.###.#.###### 1,3,1,6
            ####.#...#... 4,1,1
            #....######..#####. 1,6,5
            .###.##....# 3,2,1
        """.Split('\n',StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        
        var isValid = sut.IsValidRecord(input);

        Assert.IsTrue(isValid);

    }

    [Test]
    public void Verify_GeneratePotentialSolutions()
    {

        var testString = "???.### 1,1,3";

        var outList = sut.GeneratePotentialSolutions(testString);

        // foreach (var item in outList)
        // {
        //     System.Console.WriteLine(item);
        // }

        Assert.That(outList.Count(), Is.EqualTo(8));


    }

    [Test]
    public void Verify_CountValidSolutions()
    {
        var testString = ".??..??...?##. 1,1,3";

        var countValidSolutions = sut.CountValidSolutions(testString);

        Assert.That(countValidSolutions, Is.EqualTo(4));

    }

    [Test]
    public void Verify_CountValidSolutions_Scenario2()
    {
        var testString = "?#?#?#?#?#?#?#? 1,3,1,6";

        var countValidSolutions = sut.CountValidSolutions(testString);

        Assert.That(countValidSolutions, Is.EqualTo(1));

    }

     [Test]
    public void Verify_CountValidSolutions_Scenario3()
    {
        var testString = "????.######..#####. 1,6,5";

        var countValidSolutions = sut.CountValidSolutions(testString);

        Assert.That(countValidSolutions, Is.EqualTo(4));

    }

    [Test]
    public void Verify_CountValidSolutions_Scenario4()
    {
        var testString = "?###???????? 3,2,1";

        var countValidSolutions = sut.CountValidSolutions(testString);

        Assert.That(countValidSolutions, Is.EqualTo(10));

    }



}

/** 

NOTES from the problem:

Condition of the springs that are damaged -- the input for the puzzle

todo:  repair the damaged records

Spring status indicators:
- operational with a '.'
- damaged with a '#'
- unknown status with a '?'

At the end of each row, the size of each contiguous group of damaged springs is listed.  List acounts for the entire size of the group

For each input, count and report the number of combinations that meet the input criteria

Report the sum of all of the counts of combinations for all of the rows


???.### 1,1,3
#...### 1,1,3 X
##..### 1,1,3 X
###.### 1,1,3 X
.#..### 1,1,3 X
.##.### 1,1,3 X
..#.### 1,1,3 X
#.#.### 1,1,3 O
....### 1,1,3 X

**/

