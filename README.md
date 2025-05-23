﻿# Expense Tracker CLI

Una aplicación simple de línea de comandos para gestionar tus finanzas personales.  
Permite registrar, actualizar, eliminar y visualizar tus gastos, además de generar resúmenes generales o por mes.

---

## Características

- Agregar un gasto con descripción y monto.
- Actualizar un gasto existente.
- Eliminar un gasto por ID.
- Ver todos los gastos registrados.
- Ver el resumen total de gastos.
- Ver el resumen de gastos de un mes específico del año actual.

---

## Comandos y ejemplos de uso

```bash
$ expense-tracker add --description "Lunch" --amount 20
# Expense added successfully (ID: 1)

$ expense-tracker add --description "Dinner" --amount 10
# Expense added successfully (ID: 2)

$ expense-tracker list
# ID  Date        Description  Amount
# 1   2024-08-06  Lunch         $20
# 2   2024-08-06  Dinner        $10

$ expense-tracker summary
# Total expenses: $30

$ expense-tracker delete --id 2
# Expense deleted successfully

$ expense-tracker summary
# Total expenses: $20
````
---

# Expense Tracker CLI

A simple command-line application to manage your personal finances.  
It allows you to record, update, delete, and view your expenses, as well as generate summary reports, either overall or by month.

---

## Features

- Add an expense with a description and amount.
- Update an existing expense.
- Delete an expense by ID.
- View all recorded expenses.
- View a summary of all expenses.
- View a summary of expenses for a specific month of the current year.

---

## Commands and usage examples

```bash
$ expense-tracker add --description "Lunch" --amount 20
# Expense added successfully (ID: 1)

$ expense-tracker add --description "Dinner" --amount 10
# Expense added successfully (ID: 2)

$ expense-tracker list
# ID  Date        Description  Amount
# 1   2024-08-06  Lunch        $20
# 2   2024-08-06  Dinner       $10

$ expense-tracker summary
# Total expenses: $30

$ expense-tracker delete --id 2
# Expense deleted successfully

$ expense-tracker summary
# Total expenses: $20

$ expense-tracker summary --month 8
# Total expenses for August: $20

```
---


https://roadmap.sh/projects/expense-tracker
