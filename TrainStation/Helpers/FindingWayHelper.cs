using System.Collections.Generic;
using TrainStation.Models;

namespace TrainStation.Helpers
{
    /// <summary>
    /// Класс для поиска пути
    /// </summary>
    public class FindWayHelper {
        public readonly Graph Graph = new Graph();

        /// <summary>
        /// Модель станции переводится в модель графа для облегчения работы алгоритма поиска пути
        /// </summary>
        public FindWayHelper() {
            foreach (var point in StationStructure.Points) {
                Graph.AddVertex(point.index);
            }

            foreach (var line in StationStructure.Lines) {
                Graph.AddEdge(line.firstPoint.index, line.secondPoint.index, line.length);
            }
        }
        /// <summary>
        /// Поиск кратчайшего пути (алгоритм Дейкстры)
        /// </summary>
        /// <param name="fromIndex"> Откуда </param>
        /// <param name="toIndex"> Куда </param>
        /// <returns> Список всех точек кратчайшего пути</returns>
        public List<int> FindShortestWay(int fromIndex, int toIndex) {
            var dijkstra = new Dijkstra(Graph);
            var path = dijkstra.FindShortestPath(fromIndex, toIndex);
            return path;
        }
    }

    #region Модель поиска пути
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class GraphEdge
    {
        /// <summary>
        /// Связанная вершина
        /// </summary>
        public GraphVertex ConnectedVertex { get; }

        /// <summary>
        /// Вес ребра
        /// </summary>
        public double EdgeWeight { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="connectedVertex">Связанная вершина</param>
        /// <param name="weight">Вес ребра</param>
        public GraphEdge(GraphVertex connectedVertex, double weight) {
            ConnectedVertex = connectedVertex;
            EdgeWeight = weight;
        }
    }

    /// <summary>
    /// Вершина графа
    /// </summary>
    public class GraphVertex
    {
        /// <summary>
        /// Индекс вершины
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Список ребер
        /// </summary>
        public List<GraphEdge> Edges { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertexIndex">Название вершины</param>
        public GraphVertex(int vertexIndex) {
            Index = vertexIndex;
            Edges = new List<GraphEdge>();
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="newEdge">Ребро</param>
        public void AddEdge(GraphEdge newEdge) {
            Edges.Add(newEdge);
        }

        /// <summary>
        /// Добавить ребро
        /// </summary>
        /// <param name="vertex">Вершина</param>
        /// <param name="edgeWeight">Вес</param>
        public void AddEdge(GraphVertex vertex, double edgeWeight) {
            AddEdge(new GraphEdge(vertex, edgeWeight));
        }
    }

    /// <summary>
    /// Информация о вершине
    /// </summary>
    public class GraphVertexInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public GraphVertex Vertex { get; set; }

        /// <summary>
        /// Не посещенная вершина
        /// </summary>
        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Сумма весов ребер
        /// </summary>
        public double EdgesWeightSum { get; set; }

        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public GraphVertex PreviousVertex { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public GraphVertexInfo(GraphVertex vertex) {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }

    /// <summary>
    /// Граф
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Список вершин графа
        /// </summary>
        public List<GraphVertex> Vertices { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Graph() {
            Vertices = new List<GraphVertex>();
        }

        /// <summary>
        /// Добавление вершины
        /// </summary>
        /// <param name="vertexIndex">Имя вершины</param>
        public void AddVertex(int vertexIndex) {
            Vertices.Add(new GraphVertex(vertexIndex));
        }

        /// <summary>
        /// Поиск вершины
        /// </summary>
        /// <param name="vertexIndex">Название вершины</param>
        /// <returns>Найденная вершина</returns>
        public GraphVertex FindVertex(int vertexIndex) {
            foreach (var v in Vertices) {
                if (v.Index == vertexIndex) {
                    return v;
                }
            }

            return null;
        }

        /// <summary>
        /// Добавление ребра
        /// </summary>
        /// <param name="firstIndex">Имя первой вершины</param>
        /// <param name="secondIndex">Имя второй вершины</param>
        /// <param name="weight">Вес ребра соединяющего вершины</param>
        public void AddEdge(int firstIndex, int secondIndex, double weight) {
            var v1 = FindVertex(firstIndex);
            var v2 = FindVertex(secondIndex);
            if (v2 != null && v1 != null) {
                v1.AddEdge(v2, weight);
                v2.AddEdge(v1, weight);
            }
        }
    }

    /// <summary>
    /// Алгоритм Дейкстры
    /// </summary>
    public class Dijkstra
    {
        private readonly Graph graph;
        private List<GraphVertexInfo> infos;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Граф</param>
        public Dijkstra(Graph graph) {
            this.graph = graph;
        }

        /// <summary>
        /// Инициализация информации
        /// </summary>
        public void InitInfo() {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Vertices) {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        /// <summary>
        /// Получение информации о вершине графа
        /// </summary>
        /// <param name="v">Вершина</param>
        /// <returns>Информация о вершине</returns>
        public GraphVertexInfo GetVertexInfo(GraphVertex v) {
            foreach (var i in infos) {
                if (i.Vertex.Index == v.Index) {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск непосещенной вершины с минимальным значением суммы
        /// </summary>
        /// <returns>Информация о вершине</returns>
        public GraphVertexInfo FindUnvisitedVertexWithMinSum() {
            var minValue = double.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos) {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue) {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        /// <summary>
        /// Поиск кратчайшего пути по названиям вершин
        /// </summary>
        /// <param name="startIndex">Название стартовой вершины</param>
        /// <param name="finishIndex">Название финишной вершины</param>
        /// <returns>Кратчайший путь</returns>
        public List<int> FindShortestPath(int startIndex, int finishIndex) {
            return FindShortestPath(graph.FindVertex(startIndex), graph.FindVertex(finishIndex));
        }

        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public List<int>  FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex) {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true) {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null) {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
        public void SetSumToNextVertex(GraphVertexInfo info) {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.Edges) {
                var nextInfo = GetVertexInfo(e.ConnectedVertex);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum) {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        /// <summary>
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
        public List<int> GetPath(GraphVertex startVertex, GraphVertex endVertex) {
            var path = new List<int> { endVertex.Index };
            while (startVertex.Index != endVertex.Index) {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;
                path.Add(endVertex.Index);
            }

            return path;
        }
    }
    #endregion
}
