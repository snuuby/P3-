import React, {useEffect, useState} from 'react';
import {Table, TableBody, TableCell, TablePagination, TableRow, Checkbox} from '@material-ui/core';
import {FuseScrollbars, FuseUtils} from '@fuse';
import {withRouter} from 'react-router-dom';
import _ from '@lodash';
import TaskOverviewTableHead from './TaskOverviewTableHead';
//import OrdersStatus from '../order/OrdersStatus';
import * as Actions from '../store/actions';
import {useDispatch, useSelector} from 'react-redux';

function TaskOverviewTable(props)
{
    const dispatch = useDispatch();
    const movings = useSelector(({ taskReducer }) => taskReducer.tasks.movingTasks);
    const deliveries = useSelector(({ taskReducer }) => taskReducer.tasks.deliveryTasks);
    const tasks = checkTasksLengths();

    //const movingTasks = useSelector(({ taskReducer }) => taskReducer.tasks.movingTasks);
    //const tasks = deliveryTasks.concat(movingTasks);
    const searchText = useSelector(({ taskReducer }) => taskReducer.tasks.searchText);



    const [selected, setSelected] = useState([]);
    const [data, setData] = useState(tasks);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [task, setTask] = useState({
        direction: 'asc',
        id       : null
    });
    function checkTasksLengths() {
        if (!Array.isArray(movings) || !Array.isArray(deliveries)) {
            return movings + deliveries;
        }
        else {
            return movings.concat(deliveries);
        }
    }
    useEffect(() => {
        dispatch(Actions.getAllTasks());
    }, [dispatch]);

    useEffect(() => {
        setData(searchText.length === 0 ? tasks : FuseUtils.filterArrayByString(tasks, searchText))
    }, [ tasks, searchText]); 

    function handleRequestSort(event, property)
    {
        const id = property;
        let direction = 'desc';

        if ( task.id === property && task.direction === 'desc' )
        {
            direction = 'asc';
        }

        setTask({
            direction,
            id
        });
    }

    function handleSelectAllClick(event)
    {
        if ( event.target.checked )
        {
            setSelected(data.map(n => n.id));
            return;
        }
        setSelected([]);
    }

    // Det er ved click, måske mere customer information herinde?
    function handleClick(item)
    {
        props.history.push('/tasks/' + item.ID);
    }

    function handleCheck(event, id)
    {
        const selectedIndex = selected.indexOf(id);
        let newSelected = [];

        if ( selectedIndex === -1 )
        {
            newSelected = newSelected.concat(selected, id);
        }
        else if ( selectedIndex === 0 )
        {
            newSelected = newSelected.concat(selected.slice(1));
        }
        else if ( selectedIndex === selected.length - 1 )
        {
            newSelected = newSelected.concat(selected.slice(0, -1));
        }
        else if ( selectedIndex > 0 )
        {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1)
            );
        }

        setSelected(newSelected);
    }

    function handleChangePage(event, page)
    {
        setPage(page);
    }

    function handleChangeRowsPerPage(event)
    {
        setRowsPerPage(event.target.value);
    }

    return (
        <div className="w-full flex flex-col">

            <FuseScrollbars className="flex-grow overflow-x-auto">

                <Table className="min-w-xl" aria-labelledby="tableTitle">

                    <TaskOverviewTableHead
                        numSelected={selected.length}
                        task={task}
                        onSelectAllClick={handleSelectAllClick}
                        onRequestSort={handleRequestSort}
                        rowCount={tasks.length}
                    />

                    <TableBody>
                        {
                            _.orderBy(data, [
                                (e) => {
                                    switch ( task.id )
                                    {
                                        case 'id':
                                        {
                                            return parseInt(e.ID, 10);
                                        }
                                        case 'navn':
                                        {
                                                return e.Name;
                                        }
                                        case 'type':
                                        {
                                            return e.TaskType;
                                        }
                                        case 'lentboxes':
                                        {
                                            return e.LentBoxes;
                                        }

                                        default:
                                        {
                                            return e[task.id];
                                        }
                                    }
                                }
                            ], [task.direction])
                                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                .map(n => {
                                    const isSelected = selected.indexOf(n.id) !== -1;
                                    return (
                                        <TableRow
                                            className="h-64 cursor-pointer"
                                            hover
                                            role="checkbox"
                                            aria-checked={isSelected}
                                            tabIndex={-1}
                                            key={n.id}
                                            selected={isSelected}
                                            onClick={event => handleClick(n)}
                                        >
                                            <TableCell className="w-48 pl-4 sm:pl-12" padding="checkbox">
                                                <Checkbox
                                                    checked={isSelected}
                                                    onClick={event => event.stopPropagation()}
                                                    onChange={event => handleCheck(event, n.id)}
                                                />
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {n.ID}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {n.Name}
                                            </TableCell>


                                            <TableCell component="th" scope="row">
                                                <span></span>
                                                {n.TaskType == "Moving" ? "Flytning" : "Levering"}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {n.LentBoxes}
                                            </TableCell>


                                        </TableRow>
                                    );
                                })}
                    </TableBody>
                </Table>
            </FuseScrollbars>

            <TablePagination
                component="div"
                count={tasks.length}
                rowsPerPage={rowsPerPage}
                page={page}
                backIconButtonProps={{
                    'aria-label': 'Previous Page'
                }}
                nextIconButtonProps={{
                    'aria-label': 'Next Page'
                }}
                onChangePage={handleChangePage}
                onChangeRowsPerPage={handleChangeRowsPerPage}
            />
        </div>
    );
}

export default withRouter(TaskOverviewTable);
