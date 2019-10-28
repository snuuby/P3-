import React from 'react';
import {Paper, Input, Icon, Typography, Button, Fab} from '@material-ui/core';
import {ThemeProvider} from '@material-ui/styles';
import {FuseAnimate} from '@fuse';
import * as Actions from './store/actions';
import {useDispatch, useSelector} from 'react-redux';
import AddDialog from "./AddDialog";



function ToolOverviewHeader(props)
{
    const dispatch = useDispatch();
    const searchText = useSelector(({toolReducer}) => toolReducer.tools.searchText);
    const mainTheme = useSelector(({fuse}) => fuse.settings.mainTheme);

    return (
        <div className="flex flex-1 w-full items-center justify-between">

            <div className="flex items-center">

                <FuseAnimate animation="transition.expandIn" delay={300}>
                    <Icon className="text-32 mr-0 sm:mr-12">shopping_basket</Icon>
                </FuseAnimate>

                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                    <Typography className="hidden sm:flex" variant="h6">Udstyr</Typography>
                </FuseAnimate>
            </div>

            <div className="flex flex-1 items-center justify-center pr-0 pl-12 sm:px-12">

                <ThemeProvider theme={mainTheme}>
                    <Button
                        onClick={() => dispatch(Actions.openNewAddDialog({
                            start: new Date(),
                            end  : new Date()
                        }))}
                        variant="contained" color="green" className="max-w-512 px-8 py-100 hidden sm:flex">
                        Tilføj Udstyr
                    </Button>
                    
                    <FuseAnimate animation="transition.slideDownIn" delay={300}>
                        <Paper className="flex items-center w-full max-w-512 px-8 py-4 rounded-8" elevation={1}>
                            
                            
                            <Icon className="mr-8" color="action">search</Icon>

                            
                            <Input
                                placeholder="Search"
                                className="flex flex-1"
                                disableUnderline
                                fullWidth
                                value={searchText}
                                inputProps={{
                                    'aria-label': 'Search'
                                }}
                                onChange={ev => dispatch(Actions.setToolOverviewSearchText(ev))}
                            />
                        </Paper>
                    </FuseAnimate>
                </ThemeProvider>

                <AddDialog/>
            </div>
        </div>
        
        
    );
}

export default ToolOverviewHeader;
