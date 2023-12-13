

using System.Collections.Concurrent;

namespace AoCDay12Tests;

public class RecordParser
{

	public bool IsValidRecord(string recordString)
	{

		bool outValid = true;

		// This is valid:  #.#.### 1,1,3
		var segments = recordString.Split(' ');
		var damagedSegments = segments[1].Split(',').Select(c => int.Parse(c)).ToArray();

		var damagedPos = 0;
		var countContiguousDamaged = 0;
		foreach (var pos in segments[0])
		{

			if (pos == '#')
			{
				countContiguousDamaged++;
			}
			else
			{ // found a '.'

				if (countContiguousDamaged > 0 && damagedSegments[damagedPos] == countContiguousDamaged)
				{
					// this is valid
					damagedPos++;
					countContiguousDamaged = 0;
				}
				else if (countContiguousDamaged > 0 && damagedSegments[damagedPos] != countContiguousDamaged)
				{
					// this is invalid
					outValid = false;
					break;
				}

			}

		}

		// Run the check one final time for the last entries on the right of the string
		if (outValid && countContiguousDamaged > 0 && damagedSegments[damagedPos] == countContiguousDamaged)
		{
			// this is valid
			damagedPos++;
			countContiguousDamaged = 0;
		}
		else if (outValid && countContiguousDamaged > 0 && damagedSegments[damagedPos] != countContiguousDamaged)
		{
			// this is invalid
			outValid = false;
		}


		return outValid;


	}

	public bool IsValidRecord(IEnumerable<string> records)
	{

		bool isValid = true;
		foreach (var item in records)
		{

			var thisItemValid = IsValidRecord(item);
			if (!thisItemValid) Console.WriteLine($"This record is invalid: '{item}'");
			isValid = isValid && thisItemValid;

		}

		return isValid;

	}

	public IEnumerable<string> GeneratePotentialSolutions(string recordString)
	{

		var outList = new ConcurrentBag<string>();

		// ???.### 1,1,3
		var segments = recordString.Split(' ');
		var testString = segments[0];
		Parallel.For(0, (int)testString.Length, new ParallelOptions {
			MaxDegreeOfParallelism = segments[0].Count(s => s == '?')
		}, pos =>
		{

			if (testString[pos] == '?')
			{

				var thisHashTest = testString.Substring(0, pos) + '#' + testString.Substring(pos + 1) + ' ' + segments[1];
				if (thisHashTest.Contains('?'))
				{
					var others = AddOtherCombinations(thisHashTest);
					foreach (var item in others)
					{
						outList.Add(item);
					}
				}
				else
				{
					outList.Add(thisHashTest);
				}

				var thisDotTest = testString.Substring(0, pos) + '.' + testString.Substring(pos + 1) + ' ' + segments[1];
				if (thisDotTest.Contains('?'))
				{
					var others = AddOtherCombinations(thisDotTest);
					foreach (var item in others)
					{
						outList.Add(item);
					}
				}
				else
				{
					outList.Add(thisDotTest);
				}

			}

		});

		return outList.Distinct();

	}

	// #??.###
	private IEnumerable<string> AddOtherCombinations(string thisTest)
	{

		var outList = new HashSet<string>();

		var segments = thisTest.Split(' ');
		thisTest = segments[0];

		for (var pos = 0; pos < thisTest.Length; pos++)
		{

			if (thisTest[pos] == '?')
			{

				var thisHashTest = thisTest.Substring(0, pos) + '#' + thisTest.Substring(pos + 1) + ' ' + segments[1];
				if (thisHashTest.Contains('?'))
				{
					outList = outList.Union(AddOtherCombinations(thisHashTest)).ToHashSet();
				}
				else
				{
					outList.Add(thisHashTest);
				}

				var thisDotTest = thisTest.Substring(0, pos) + '.' + thisTest.Substring(pos + 1) + ' ' + segments[1];
				if (thisDotTest.Contains('?'))
				{
					outList = outList.Union(AddOtherCombinations(thisDotTest)).ToHashSet();
				}
				else
				{
					outList.Add(thisDotTest);
				}

			}

		}

		return outList;

	}

    public int CountValidSolutions(string testString)
    {

			var potentialSolutions = GeneratePotentialSolutions(testString);

			Console.WriteLine($"Inspecting {potentialSolutions.Count()} solutions");

			return potentialSolutions.Count(p => IsValidRecord(p));

    }
}
