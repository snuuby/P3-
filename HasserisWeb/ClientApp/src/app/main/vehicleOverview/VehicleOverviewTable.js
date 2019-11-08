import React, {useEffect, useState} from 'react';
import {Table, TableBody, TableCell, TablePagination, TableRow, Checkbox} from '@material-ui/core';
import {FuseScrollbars, FuseUtils} from '@fuse';
import {withRouter} from 'react-router-dom';
import _ from '@lodash';
import VehicleOverviewTableHead from './VehicleOverviewTableHead';
//import OrdersStatus from '../order/OrdersStatus';
import * as Actions from './store/actions';
import {useDispatch, useSelector} from 'react-redux';

function VehicleOverviewTable(props)
{
    const dispatch = useDispatch();
    const vehicles = useSelector(({vehicleReducer}) => vehicleReducer.vehicles.entities);
    const searchText = useSelector(({vehicleReducer}) => vehicleReducer.vehicles.searchText);
    //const searchText = useSelector(({eCommerceApp}) => eCommerceApp.orders.searchText);

    const [selected, setSelected] = useState([]);
    const [data, setData] = useState(vehicles);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [vehicle, setVehicle] = useState({
        direction: 'asc',
        id       : null
    });

    useEffect(() => {
        dispatch(Actions.getVehicles());
    }, [dispatch]);

    useEffect(() => {
        setData(searchText.length === 0 ? vehicles : FuseUtils.filterArrayByString(vehicles, searchText))
    }, [vehicles, searchText]);

    function handleRequestSort(event, property)
    {
        const id = property;
        let direction = 'desc';

        if ( vehicle.id === property && vehicle.direction === 'desc' )
        {
            direction = 'asc';
        }

        setVehicle({
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

    // Det er ved click, måske mere vehicle information herinde?
    function handleClick(item)
    {
        props.history.push('/vehicle/' + item.ID);
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

                    <VehicleOverviewTableHead
                        numSelected={selected.length}
                        vehicle={vehicle}
                        onSelectAllClick={handleSelectAllClick}
                        onRequestSort={handleRequestSort}
                        rowCount={data.length}
                    />

                    <TableBody>
                        {
                            _.orderBy(data, [
                                (e) => {
                                    switch ( vehicle.id )
                                    {
                                        case 'id':
                                        {
                                            return parseInt(e.id, 10);
                                        }
                                        case 'navn':
                                        {
                                            return e.name;
                                        }
                                        case 'model':
                                        {
                                            return e.model;
                                        }
                                        case 'nummerplade':
                                        {
                                            return e.regNum;
                                        }
                                        default:
                                        {
                                            return e[vehicle.id];
                                        }
                                    }
                                }
                            ], [vehicle.direction])
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
                                                {n.id}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {n.name}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {n.model}
                                            </TableCell>

                                            <TableCell component="th" scope="row" align="right">
                                                <span>$</span>
                                                {n.regNum}
                                            </TableCell>
                                                
                                        </TableRow>
                                    );
                                })}
                    </TableBody>
                </Table>
            </FuseScrollbars>

            <TablePagination
                component="div"
                count={data.length}
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

export default withRouter(VehicleOverviewTable);
