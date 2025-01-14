<h2>Description</h2>
<p>The <strong>DSproject</strong> is a comprehensive implementation of fundamental data structures and algorithms in C#. It includes efficient solutions for operations such as insertion, deletion, and search across various structures like stacks, queues, binary trees, and graphs. Additionally, the project provides indexing mechanisms to optimize operations for databases.</p>

<hr>

<h2>Features</h2>
<ul>
    <li><strong>Data Structures:</strong>
        <ul>
            <li><strong>Stack:</strong> Supports <code>Push</code> and <code>Pop</code> operations.</li>
            <li><strong>Queue:</strong> Implements <code>Enqueue</code> and <code>Dequeue</code>.</li>
            <li><strong>Binary Search Tree:</strong> Includes traversal methods such as PreOrder, InOrder, and PostOrder.</li>
            <li><strong>Hash Table:</strong> Efficient lookups for key-value pairs.</li>
        </ul>
    </li>
    <li><strong>Database Indexing:</strong>
        <ul>
            <li><strong>Unique Index:</strong> Fast lookups for unique keys.</li>
            <li><strong>Non-Unique Index:</strong> Handles multiple rows for the same key.</li>
            <li><strong>Range Index:</strong> Supports range-based queries.</li>
        </ul>
    </li>
    <li><strong>Graph Algorithms:</strong>
        <ul>
            <li>Depth-First Search (DFS).</li>
            <li>Breadth-First Search (BFS).</li>
        </ul>
    </li>
</ul>

<hr>

<h2>Installation and Usage</h2>

<h3>Clone the Repository</h3>
<pre><code>git clone https://github.com/eveGeno/DSproject.git
cd DSproject </code></pre>
  
<h3>Build the Project</h3>
<ol>
    <li>Open the <code>DSproject.sln</code> file in Visual Studio or your preferred IDE.</li>
    <li>Build the solution.</li>
    <li>Run the project to see the functionality in action.</li>
</ol>

<h3>Example Usage</h3>
<pre><code>Graph g = new Graph();
g.GraphFunc(2, Console.WriteLine); </code></pre>
  
<hr>

<h2>Project Structure</h2>
<ul>
    <li><strong>Customer.cs:</strong> Contains the <code>Customer</code> data class with properties like <code>Id</code>, <code>CompanyName</code>, <code>ContactName</code>, and more.</li>
    <li><strong>DataBaseEngine.cs:</strong> Implements the core database engine for managing tables and indices.</li>
    <li><strong>NonUniqueIndex.cs:</strong> Provides support for indices with non-unique keys.</li>
    <li><strong>RangeIndex.cs:</strong> Handles range-based queries on database indices.</li>
    <li><strong>UniqueIndex.cs:</strong> Manages indices with unique keys.</li>
    <li><strong>Graph.cs:</strong> Includes graph data structure and traversal methods.</li>
    <li><strong>Program.cs:</strong> Entry point of the application with demonstrations of implemented functionality.</li>
</ul>

<hr>

<h2>Testing</h2>
<ol>
    <li>Build and run the solution in your IDE.</li>
    <li>Check console output for results.</li>
    <li>Add your own test cases to validate specific scenarios.</li>
</ol>

<hr>

<h2>Requirements</h2>
<ul>
    <li>.NET Framework 6.0 or higher.</li>
    <li>IDE with C# support (e.g., Visual Studio, Rider).</li>
</ul>

<hr>

<h2>Contribution</h2>
<p>Contributions are welcome! Feel free to open issues or submit pull requests to improve the project.</p>

<hr>

<h2>License</h2>
<p>This project is licensed under the MIT License. See the <a href="https://github.com/eveGeno/DSproject/blob/main/LICENSE">LICENSE</a> file for details.</p>

<hr>

<h2>Contact</h2>
<p>For any questions or feedback, reach out:</p>
<ul>
    <li>GitHub: <a href="https://github.com/eveGeno">eveGeno</a></li>
</ul>
