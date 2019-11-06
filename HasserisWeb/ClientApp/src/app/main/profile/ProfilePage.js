import React, {useState} from 'react';
import {Avatar, Button, Tab, Tabs, Typography} from '@material-ui/core';
import {makeStyles} from '@material-ui/styles';
import {FusePageSimple, FuseAnimate} from '@fuse';
import TimelineTab from './tabs/TimelineTab';
import PhotosVideosTab from './tabs/PhotosVideosTab';
import AboutTab from './tabs/AboutTab';
import { useDispatch, useSelector } from 'react-redux';
import * as Action from 'app/auth/store/actions';
//import { dispatch } from 'rxjs/internal/observable/pairs';

const useStyles = makeStyles(theme => ({
    layoutHeader: {
        height                        : 320,
        minHeight                     : 320,
        [theme.breakpoints.down('md')]: {
            height   : 240,
            minHeight: 240
        }
    }
}));

function ProfilePage()
{
    const classes = useStyles();
    const dispatch = useDispatch();
    const [selectedTab, setSelectedTab] = useState(0);
    const user = useSelector(({ auth }) => auth.user);
    function handleTabChange(event, value)
    {
        setSelectedTab(value);
    }
    let contentElement = document.getElementById("content");

    function getExtension(fileName) {
        var ext = fileName.split('.');
        return ext[ext.length - 1];
    }

    // Button callback
    async function onAvatarClicked() {
        var file = await selectFile("image/jpeg, image/png, image/jpg", false);
        // Filtype check anvendt fra https://stackoverflow.com/questions/7977084/check-file-type-when-form-submit
        var extension = getExtension(file.name);
        //alert(`filename ${file.name}, extension is ${extension}`)
        switch (extension.toLowerCase()) {
            case "jpg":
            case "png":
            case "jpeg":
                let fileURL = URL.createObjectURL(file);
                //document.getElementById("profileimg").src = fileURL;
                dispatch(Action.setUserImage(fileURL, user.data.displayName, "avatars", file.type));
                if (window.confirm("Profil billede opdateret.\nSiden skal genindlaeses for at vise billedet.\nGenindlaes side nu?")) {
                    document.location.reload();
                }
                break;
            default:
                alert("Filtype ikke supportet. Supportet filtyper:\nJPG, JPEG, PNG")
        }
    }

    // ---- function definition ----
    function selectFile(contentType, multiple) {
        return new Promise((resolve, reject) => {
            let input = document.createElement('input');
            input.type = 'file';
            input.multiple = false;
            input.accept = contentType;

            input.onchange = _ => {
                let files = Array.from(input.files);
                if (multiple)
                    resolve(files);
                else
                    resolve(files[0]);
                input.error && reject(files);
            };
            
            input.click();
            

        });
    }
    return (
        <FusePageSimple
            classes={{
                header : classes.layoutHeader,
                toolbar: "px-16 sm:px-24"
            }}
            header={
                <div className="p-24 flex flex-1 flex-col items-center justify-center md:flex-row md:items-end">
                    <div className="flex flex-1 flex-col items-center justify-center md:flex-row md:items-center md:justify-start">
                        <FuseAnimate animation="transition.expandIn" delay={300}>
                            <Avatar onClick={onAvatarClicked} className="w-96 h-96" >
                                <img id="profileimg" src={user.data.photoURL} height="96" width="96" />
                            </Avatar>
                        </FuseAnimate>
                        <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                            <Typography className="md:ml-24" variant="h4" color="inherit">{user.data.firstName} {user.data.lastName}</Typography>
                        </FuseAnimate>
                    </div>

                    <div className="flex items-center justify-end">
                        <Button className="mr-8 normal-case" variant="contained" color="secondary" aria-label="Follow">Follow</Button>
                        <Button className="normal-case" variant="contained" color="primary" aria-label="Send Message">Send Message</Button>
                    </div>
                </div>
            }
            contentToolbar={
                <Tabs
                    value={selectedTab}
                    onChange={handleTabChange}
                    indicatorColor="secondary"
                    textColor="secondary"
                    variant="scrollable"
                    scrollButtons="off"
                    classes={{
                        root: "h-64 w-full border-b-1"
                    }}
                >
                    <Tab
                        classes={{
                            root: "h-64"
                        }}
                        label="Timeline"/>
                    <Tab
                        classes={{
                            root: "h-64"
                        }} label="About"/>
                    <Tab
                        classes={{
                            root: "h-64"
                        }} label="Photos & Videos"/>
                </Tabs>
            }
            content={
                <div className="p-16 sm:p-24">
                    {selectedTab === 0 &&
                    (
                        <TimelineTab/>
                    )}
                    {selectedTab === 1 && (
                        <AboutTab/>
                    )}
                    {selectedTab === 2 && (
                        <PhotosVideosTab/>
                    )}
                </div>
            }
        />
    )
}

export default ProfilePage;
