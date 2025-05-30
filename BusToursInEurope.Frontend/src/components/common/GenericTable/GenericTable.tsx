import React from "react";
import styles from "./styles.module.css";

interface ColumnDefinition<T> {
  key: string;
  title: string;
  render?: (item: T) => React.ReactNode;
}

interface GenericTableProps<T> {
  data: T[];
  columns: ColumnDefinition<T>[];
  className?: string;
}

export const GenericTable = <T,>({ data, columns, className = "" }: GenericTableProps<T>) => {
  return (
    <div className={`${styles.tableContainer} ${className}`}>
      <table className={styles.table}>
        <thead className={styles.tableHead}>
          <tr>
            {columns.map((column) => (
              <th
                key={column.key}
                scope="col"
                className={styles.tableHeaderCell}
              >
                {column.title}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className={styles.tableBody}>
          {data.map((item, index) => (
            <tr key={index} className={styles.tableRow}>
              {columns.map((column) => (
                <td key={`${index}-${column.key}`} className={styles.tableCell}>
                  {column.render
                    ? column.render(item)
                    : (item as Record<string, any>)[column.key]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};