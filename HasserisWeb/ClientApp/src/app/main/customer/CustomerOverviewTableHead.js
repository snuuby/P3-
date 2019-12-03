import React, {useState} from 'react';
import {TableHead, TableSortLabel, TableCell, TableRow, Checkbox, Tooltip, IconButton, Icon, Menu, MenuList, MenuItem, ListItemIcon, ListItemText,} from '@material-ui/core';
import clsx from 'clsx';
import {makeStyles} from '@material-ui/styles';

const rows = [
    {
        id            : 'id',
        align         : 'left',
        disablePadding: false,
        label         : 'ID',
        sort          : true
    },
    {
        id            : 'navn',
        align         : 'left',
        disablePadding: false,
        label         : 'Navn',
        sort          : true
    },
    {
        id            : 'type',
        align         : 'left',
        disablePadding: false,
        label         : 'Type',
        sort          : true
    },
    {
        id            : 'lentboxes',
        align         : 'left',
        disablePadding: false,
        label         : 'Lånte kasser',
        sort          : true
    }, 
    {
        id: 'email',
        align: 'left',
        disablePadding: false,
        label: 'E-mail',
        sort: true
    },
    {
        id: 'phonenumber',
        align: 'left',
        disablePadding: false,
        label: 'Telefon',
        sort: true
    },
    {
        id: 'CVR/EAN',
        align: 'left',
        disablePadding: false,
        label: 'CVR/EAN',
        sort: true
    },
];

const useStyles = makeStyles(theme => ({
    actionsButtonWrapper: {
        background: theme.palette.background.paper
    }
}));

function CustomerOverviewTableHead(props)
{
    const classes = useStyles(props);
    const [selectedOrdersMenu, setSelectedOrdersMenu] = useState(null);

    const createSortHandler = property => event => {
        props.onRequestSort(event, property);
    };

    function openSelectedOrdersMenu(event)
    {
        setSelectedOrdersMenu(event.currentTarget);
    }

    function closeSelectedOrdersMenu()
    {
        setSelectedOrdersMenu(null);
    }

    //const {numSelected, customer, onSelectAllClick, orderby, rowCount} = props;

    return (
        <TableHead>
            <TableRow className="h-64">
                <TableCell padding="checkbox" className="relative pl-4 sm:pl-12">
                    <Checkbox
                        indeterminate={props.numSelected > 0 && props.numSelected < props.rowCount}
                        checked={props.numSelected === props.rowCount}
                        onChange={props.onSelectAllClick}
                    />
                    {props.numSelected > 0 && (
                        <div className={clsx("flex items-center justify-center absolute w-64 top-0 left-0 ml-68 h-64 z-10", classes.actionsButtonWrapper)}>
                            <IconButton
                                aria-owns={selectedOrdersMenu ? 'selectedOrdersMenu' : null}
                                aria-haspopup="true"
                                onClick={openSelectedOrdersMenu}
                            >
                                <Icon>more_horiz</Icon>
                            </IconButton>
                            <Menu
                                id="selectedOrdersMenu"
                                anchorEl={selectedOrdersMenu}
                                open={Boolean(selectedOrdersMenu)}
                                onClose={closeSelectedOrdersMenu}
                            >
                                <MenuList>
                                    <MenuItem
                                        onClick={() => {
                                            closeSelectedOrdersMenu();
                                        }}
                                    >
                                        <ListItemIcon className="min-w-40">
                                            <Icon>delete</Icon>
                                        </ListItemIcon>
                                        <ListItemText primary="Remove"/>
                                    </MenuItem>
                                </MenuList>
                            </Menu>
                        </div>
                    )}
                </TableCell>
                {rows.map(row => {
                    return (
                        <TableCell
                            key={row.id}
                            align={row.align}
                            padding={row.disablePadding ? 'none' : 'default'}
                            sortDirection={props.customer.id === row.id ? props.customer.direction : false}
                        >
                            {row.sort && (
                                <Tooltip
                                    title="Sort"
                                    placement={row.align === "right" ? 'bottom-end' : 'bottom-start'}
                                    enterDelay={300}
                                >
                                    <TableSortLabel
                                        active={props.customer.id === row.id}
                                        direction={props.customer.direction}
                                        onClick={createSortHandler(row.id)}
                                    >
                                        {row.label}
                                    </TableSortLabel>
                                </Tooltip>
                            )}
                        </TableCell>
                    );
                }, this)}
            </TableRow>
        </TableHead>
    );
}

export default CustomerOverviewTableHead;
