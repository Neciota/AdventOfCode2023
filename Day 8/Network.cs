namespace Day_8
{
    internal class Network
    {
        public Network(IEnumerable<string> nodeInfo)
        {
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (string stepInfo in nodeInfo.Order())
            {
                string[] info = stepInfo.Split('=');
                string identifier = info[0].Trim();
                string leftIdentifier = info[1].Trim().Substring(1, 3);
                string rightIdentifier = info[1].Trim().Substring(6, 3);

                Node node = nodes.ContainsKey(identifier) ? nodes[identifier] : new Node(identifier);
                nodes[identifier] = node;

                if (node.LeftNode is null)
                {
                    node.LeftNode = nodes.ContainsKey(leftIdentifier) ? nodes[leftIdentifier] : new Node(leftIdentifier);
                    nodes[leftIdentifier] = node.LeftNode;
                }
                    
                if (node.RightNode is null)
                {
                    node.RightNode = nodes.ContainsKey(rightIdentifier) ? nodes[rightIdentifier] : new Node(rightIdentifier);
                    nodes[rightIdentifier] = node.RightNode;
                }
            }

            StartNode = nodes["AAA"];
        }

        public Node StartNode { get; private set; }

        public long StepsToEnd(Step[] steps)
        {
            Node node = StartNode;

            long stepCount = 0; 
            while (node.Identifier != "ZZZ")
            {
                Step step = steps[stepCount++ % steps.Length];
                if (step == Step.Left)
                    node = node.LeftNode ?? throw new NullReferenceException("Left child was not initialized.");
                else
                    node = node.RightNode ?? throw new NullReferenceException("Left child was not initialized.");
            }

            return stepCount;
        }
    }
}
