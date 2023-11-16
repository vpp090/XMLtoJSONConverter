import { Container, Grid } from 'semantic-ui-react'
import Uploader from '../upload/Uploader'
import './App.css'
import 'semantic-ui-css/semantic.min.css'

function App() {
  

  return (
    <Container>
        <Grid>
          <Grid.Row>
            <Grid.Column textAlign='center'><h1>XML to JSON Converter</h1></Grid.Column>
          </Grid.Row>
          <Grid.Row>
            <Grid.Column textAlign='left'>
                  <Uploader />
            </Grid.Column>
          </Grid.Row>
      </Grid>
    </Container>
    
  )
}

export default App
