import React from "react";
import styles from "./styles.module.css";

interface ColumnDefinition<T> {
  key: string;
  title: string;
  render?: (item: T) => React.ReactNode;
  width?: string;
  align?: "left" | "center" | "right";
}

interface GenericTableProps<T> {
  data: T[];
  columns: ColumnDefinition<T>[];
  className?: string;
  emptyMessage?: string;
  onRowClick?: (item: T) => void;
}

export const GenericTable = <T,>({ 
  data, 
  columns, 
  className = "", 
  emptyMessage = "Нет данных",
  onRowClick 
}: GenericTableProps<T>) => {
  return (
    <div className={`${styles.tableWrapper} ${className}`}>
      <div className={styles.tableContainer}>
        <table className={styles.table}>
          <thead className={styles.tableHeader}>
            <tr>
              {columns.map((column) => (
                <th
                  key={column.key}
                  scope="col"
                  className={styles.tableHeaderCell}
                  style={{
                    width: column.width || "auto",
                    textAlign: column.align || "left"
                  }}
                >
                  {column.title}
                </th>
              ))}
            </tr>
          </thead>
          <tbody className={styles.tableBody}>
            {data.length > 0 ? (
              data.map((item, index) => (
                <tr 
                  key={index} 
                  className={styles.tableRow}
                  onClick={() => onRowClick && onRowClick(item)}
                >
                  {columns.map((column) => (
                    <td 
                      key={`${index}-${column.key}`} 
                      className={styles.tableCell}
                      style={{ textAlign: column.align || "left" }}
                    >
                      {column.render
                        ? column.render(item)
                        : (item as Record<string, any>)[column.key]}
                    </td>
                  ))}
                </tr>
              ))
            ) : (
              <tr className={styles.emptyRow}>
                <td colSpan={columns.length} className={styles.emptyCell}>
                  {emptyMessage}
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};