import React, {useEffect, useState} from 'react';
import {Table, TableBody, TableCell, TablePagination, TableRow, Checkbox} from '@material-ui/core';
import {FuseScrollbars, FuseUtils} from '@fuse';
import {withRouter} from 'react-router-dom';
import _ from '@lodash';
import OffersOverviewTableHead from './OffersOverviewTableHead';
//import OrdersStatus from '../order/OrdersStatus';
import * as Actions from '../store/actions';
import {useDispatch, useSelector} from 'react-redux';

function OffersOverviewTable(props)
{
    const dispatch = useDispatch();
    const offers = useSelector(({ makeReducer }) => makeReducer.offers.offers);
    const searchText = useSelector(({ makeReducer }) => makeReducer.offers.searchText);

    const [selected, setSelected] = useState([]);
    const [data, setData] = useState(offers);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [offer, setOffer] = useState({
        direction: 'asc',
        id       : null
    });

    useEffect(() => {
        dispatch(Actions.getAllOffers());
    }, [dispatch]);


    useEffect(() => {
        setData(searchText.length === 0 ? offers : FuseUtils.filterArrayByString(offers, searchText))
    }, [offers, searchText]);

    function handleRequestSort(event, property)
    {
        const id = property;
        let direction = 'desc';

        if (offer.id === property && offer.direction === 'desc' )
        {
            direction = 'asc';
        }

        setOffer({
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
        props.history.push('/offers/' + item.ID);
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

                    <OffersOverviewTableHead
                        history={props.history}
                        numSelected={selected.length}
                        offer={offer}
                        onSelectAllClick={handleSelectAllClick}
                        onRequestSort={handleRequestSort}
                        rowCount={data.length}
                    />

                    <TableBody>
                        {
                            _.orderBy(data, [
                                (e) => {
                                    switch (offer.id )
                                    {
                                        case 'id':
                                        {
                                            return parseInt(e.ID, 10);
                                        }
                                        case 'inspectiondate':
                                        {
                                            return e.InspectionDate;
                                        }
                                        case 'movingdate':
                                        {
                                            return e.MovingDate;
                                        }
                                        case 'expirationdate':
                                        {
                                            return e.ExpirationDate;
                                        }
                                        case 'customer':
                                        {
                                                return e.Customer.CustomerType == "Private" ? e.Customer.Firstname + ' ' + e.Customer.Lastname : e.Customer.Name;
                                        }

                                        default:
                                        {
                                                return e[offer.id];
                                        }
                                    }
                                }
                            ], [offer.direction])
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
                                                {n.InspectionDate.toString()}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {n.MovingDate.toString()}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                <span></span>
                                                {n.ExpirationDate.toString()}
                                            </TableCell>
                                            
                                            <TableCell component="th" scope="row">
                                                
                                                { n.Customer.$type == "HasserisWeb.Private, HasserisWeb" ? n.Customer.Firstname + ' ' + n.Customer.Lastname : n.Customer.Name}
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

export default withRouter(OffersOverviewTable);
