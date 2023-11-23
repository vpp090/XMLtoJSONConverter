import axios from "axios";
import { ChangeEvent, useState } from "react";
import { Button, Form, Grid, Input, TextArea } from "semantic-ui-react";

export default function Uploader(){
    
    axios.defaults.baseURL = import.meta.env.VITE_API_URL;

    const[selectedFile, setSelectedFile] = useState<File | null>(null);
    const[responseJson, setResponse] = useState<string | null>(null);
    const[responseError, setError] = useState<string | null>(null);

    const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
        if(e.target.files)
            setSelectedFile(e.target.files[0]);
    };

    const handleUpload = () => {
        if(selectedFile){
            const formData = new FormData();
            formData.append('xmlFile', selectedFile);
            formData.append('fileName', selectedFile.name.split('.').slice(0, -1).toString());

            axios.post('/Converter/', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
            .then(response => {
                setResponse(JSON.stringify(response.data, null, 2));
                setError(null);
                
            })
            .catch(error => {
                console.error('Error uploading file', error);
                setResponse(null);
                setError(JSON.stringify(error.response.data));
            })
        }
    }
    return(
        <>
        <Grid>
            <Grid.Row>
                <Grid.Column>
                    <Form>
                        <Form.Field>
                            <label>Choose an xml file to upload</label>
                            <Input type="file" onChange={handleFileChange} />
                        </Form.Field>
                        <Button primary onClick={handleUpload}>
                            Convert XML to JSON
                        </Button>
                    </Form>
                </Grid.Column>
            </Grid.Row>
           
                
                        {responseJson && (
                            <Grid.Row>
                                <Grid.Column>
                                    <TextArea
                                    value={responseJson}
                                    placeholder="Response from the server"
                                    rows={20}
                                    style={{ width: '90%', minHeight: '200px' }}
                                    readOnly/>
                                </Grid.Column>
                               
                            </Grid.Row>
                           
                        )}

                        {responseError && (
                             <Grid.Row>
                                <Grid.Column>
                                        <h4>Error Occurred: </h4> <label style={{color: 'red'}}>{responseError}</label>
                                </ Grid.Column>
                              
                            </Grid.Row>
                        )}
                
           
        </Grid>
           
           
            
        </>
    )
}