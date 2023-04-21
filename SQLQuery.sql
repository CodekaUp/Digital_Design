-- 1. Сотрудник с максимальной заработной платой.
SELECT Name, Salary as max_salary 
FROM Employee
WHERE Salary = (Select MAX(Salary) from Employee);

-- 2. Одно число: максимальная длина цепочки руководителей по таблице сотрудников (глубина дерева).
WITH depth_tree AS (
  SELECT Id, Chief_Id, 1 AS depth
  FROM Employee
  WHERE Chief_Id IS NULL
  UNION ALL
  SELECT s.Id, s.Chief_Id, zx.depth + 1
  FROM Employee s
  JOIN depth_tree zx ON zx.Id = s.Chief_Id
)
SELECT MAX(depth) AS max_depth
FROM depth_tree;

-- 3. Отдел, с максимальной суммарной зарплатой сотрудников.
SELECT TOP 1 d.Name, SUM(e.Salary) AS sum_salary
	FROM Employee e JOIN Department d
	ON d.Id = e.Department_Id
GROUP BY d.Name
ORDER BY sum_salary DESC;

-- 4. Сотрудник, чье имя начинается на «Р» и заканчивается на «н».
SELECT Name
FROM Employee 
WHERE Name LIKE 'Р%н';
