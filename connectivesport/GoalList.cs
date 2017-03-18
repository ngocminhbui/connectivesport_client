namespace connectivesport
{
	public class GoalList
	{
		static Goal[] mGoalList =
		{
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"},
			new Goal { CustomMessage = "Hello world!"}
		};
		private Goal[] mGoals;
		public GoalList()
		{
			mGoals = mGoalList;
		}
		// Indexer (read only):

		public Goal this[int i]
		{
			get { return mGoals[i]; }
		}
		public int length
		{
			get { return mGoals.Length; }
		}

	}
}