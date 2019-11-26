import React, {Component, useEffect} from 'react';
import {withStyles} from '@material-ui/core/styles';
import {
    AppBar, Button,
    Dialog, DialogActions,
    DialogContent, Fab,
    FormControlLabel, Icon, IconButton, Select,
    Switch,
    TextField,
    Toolbar,
    Typography
} from '@material-ui/core';
import {FusePageSimple, FuseAnimate, FusePageCarded, DemoContent} from '@fuse';
import axios from 'axios';
import * as Actions from '../../store/actions';
import {useDispatch, useSelector} from "react-redux";
import reducer from '../../store/reducers';
import withReducer from "../../../../store/withReducer";
import {createStore} from "redux";
import {makeStyles} from "@material-ui/styles";
import OffersOverviewTable from './OffersOverviewTable';
import OffersOverviewTableHead from './OffersOverviewTableHead';
import OffersOverviewHeader from './OffersOverviewHeader.js';



const styles = theme => ({
    layoutRoot: {}
});


function deleteCustomer(id) {
    
    if (window.confirm("Er du sikker?")) {
        axios.post(`Customers/delete/` + id)
            .then(res => {

            });

        window.location.reload();
    } else {
        console.log("Answer was no to prompt");
    }


}

function editCustomer(id) {
    alert("Edit customer with: "  + id);
}


const useStyles = makeStyles(theme => ({
    root     : {
        '& .rbc-header'                                                                                                : {
            padding   : '12px 6px',
            fontWeight: 600,
            fontSize  : 14
        },
        '& .rbc-label'                                                                                                 : {
            padding: '8px 6px'
        },
        '& .rbc-today'                                                                                                 : {
            backgroundColor: 'transparent'
        },
        '& .rbc-header.rbc-today, & .rbc-month-view .rbc-day-bg.rbc-today'                                             : {
            borderBottom: '2px solid ' + theme.palette.secondary.main + '!important'
        },
        '& .rbc-month-view, & .rbc-time-view, & .rbc-agenda-view'                                                      : {
            padding                       : 24,
            [theme.breakpoints.down('sm')]: {
                padding: 16
            },
            ...theme.mixins.border(0)
        },
        '& .rbc-agenda-view table'                                                                                     : {
            ...theme.mixins.border(1),
            '& thead > tr > th': {
                ...theme.mixins.borderBottom(0)
            },
            '& tbody > tr > td': {
                padding : '12px 6px',
                '& + td': {
                    ...theme.mixins.borderLeft(1)
                }
            }
        },
        '& .rbc-time-view'                                                                                             : {
            '& .rbc-time-header' : {
                ...theme.mixins.border(1)
            },
            '& .rbc-time-content': {
                flex: '0 1 auto',
                ...theme.mixins.border(1)
            }
        },
        '& .rbc-month-view'                                                                                            : {
            '& > .rbc-row'               : {
                ...theme.mixins.border(1)
            },
            '& .rbc-month-row'           : {
                ...theme.mixins.border(1),
                borderWidth: '0 1px 1px 1px!important',
                minHeight  : 128
            },
            '& .rbc-header + .rbc-header': {
                ...theme.mixins.borderLeft(1)
            },
            '& .rbc-header'              : {
                ...theme.mixins.borderBottom(0)
            },
            '& .rbc-day-bg + .rbc-day-bg': {
                ...theme.mixins.borderLeft(1)
            }
        },
        '& .rbc-day-slot .rbc-time-slot'                                                                               : {
            ...theme.mixins.borderTop(1),
            opacity: 0.5
        },
        '& .rbc-time-header > .rbc-row > * + *'                                                                        : {
            ...theme.mixins.borderLeft(1)
        },
        '& .rbc-time-content > * + * > *'                                                                              : {
            ...theme.mixins.borderLeft(1)
        },
        '& .rbc-day-bg + .rbc-day-bg'                                                                                  : {
            ...theme.mixins.borderLeft(1)
        },
        '& .rbc-time-header > .rbc-row:first-child'                                                                    : {
            ...theme.mixins.borderBottom(1)
        },
        '& .rbc-timeslot-group'                                                                                        : {
            minHeight: 64,
            ...theme.mixins.borderBottom(1)
        },
        '& .rbc-date-cell'                                                                                             : {
            padding   : 8,
            fontSize  : 16,
            fontWeight: 400,
            opacity   : .5,
            '& > a'   : {
                color: 'inherit'
            }
        },
        '& .rbc-event'                                                                                                 : {
            borderRadius            : 4,
            padding                 : '4px 8px',
            backgroundColor         : theme.palette.primary.dark,
            color                   : theme.palette.primary.contrastText,
            boxShadow               : theme.shadows[0],
            transitionProperty      : 'box-shadow',
            transitionDuration      : theme.transitions.duration.short,
            transitionTimingFunction: theme.transitions.easing.easeInOut,
            position                : 'relative',
            '&:hover'               : {
                boxShadow: theme.shadows[2]
            }
        },
        '& .rbc-row-segment'                                                                                           : {
            padding: '0 4px 4px 4px'
        },
        '& .rbc-off-range-bg'                                                                                          : {
            backgroundColor: theme.palette.type === 'light' ? 'rgba(0,0,0,0.03)' : 'rgba(0,0,0,0.16)'
        },
        '& .rbc-show-more'                                                                                             : {
            color     : theme.palette.secondary.main,
            background: 'transparent'
        },
        '& .rbc-addons-dnd .rbc-addons-dnd-resizable-month-event'                                                      : {
            position: 'static'
        },
        '& .rbc-addons-dnd .rbc-addons-dnd-resizable-month-event .rbc-addons-dnd-resize-month-event-anchor:first-child': {
            left  : 0,
            top   : 0,
            bottom: 0,
            height: 'auto'
        },
        '& .rbc-addons-dnd .rbc-addons-dnd-resizable-month-event .rbc-addons-dnd-resize-month-event-anchor:last-child' : {
            right : 0,
            top   : 0,
            bottom: 0,
            height: 'auto'
        }
    },
    addButton: {
        position: 'absolute',
        right   : 12,
        top     : 172,
        zIndex  : 99
    }
}));

function OffersOverview(props) {
    // Get access to the customers
    const dispatch = useDispatch();
    const offers = useSelector(({ makeReducer }) => makeReducer.offers.offers);
    
    const classes = useStyles(props);

    useEffect(() => {
        dispatch(Actions.getAllOffers());
    }, [dispatch]);
    
 

    return(
        <FusePageCarded
            classes={{
                content: "flex",
                header : "min-h-72 h-72 sm:h-136 sm:min-h-136"
            }}
            header={
                <OffersOverviewHeader history={props.history}/>
            }
            content={
                <div>

                    <OffersOverviewTable history={props.history}/>

                    <FuseAnimate animation="transition.expandIn" delay={500}>
                        <Fab
                            color="secondary"
                            aria-label="add"
                            className={classes.addButton}
                        >
                            <Icon>add</Icon>
                        </Fab>
                    </FuseAnimate>
                    
                    
                </div>
            }
            innerScroll
            
        />
        
        
        );
    
}

export default withReducer('makeReducer', reducer)(OffersOverview);
//export default withStyles(styles, {withTheme: true})(EmployeeOverview);
